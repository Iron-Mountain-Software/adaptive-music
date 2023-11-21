using System.Collections;
using IronMountain.AdaptiveMusic.Intensity;
using IronMountain.AdaptiveMusic.Stems;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace IronMountain.AdaptiveMusic
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    public class StemPlayer : MonoBehaviour
    {
        [Header("Cache")]
        private SongPlayer _songPlayer;
        private AudioSource _audioSource;
        private Stems.AdaptiveStem _stem;

        public AudioSource AudioSource
        {
            get
            {
                if (!_audioSource) _audioSource = GetComponent<AudioSource>();
                if (!_audioSource) _audioSource = gameObject.AddComponent<AudioSource>();
                return _audioSource;
            }
        }

        private SongPlayer SongPlayer
        {
            get => _songPlayer;
            set
            {
                if (_songPlayer) _songPlayer.OnVolumeChanged -= RefreshVolume;
                _songPlayer = value;
                if (_songPlayer) _songPlayer.OnVolumeChanged += RefreshVolume;
            }
        }

        public void Initialize(SongPlayer songPlayer, Stems.AdaptiveStem stem, AudioMixerGroup audioMixerGroup)
        {
            _stem = stem;
            SongPlayer = songPlayer;
            AudioSource.outputAudioMixerGroup = audioMixerGroup;
            AudioSource.playOnAwake = false;
            AudioSource.loop = false;
            RefreshVolume();
        }

        private void OnEnable()
        {
            if (!SongPlayer) SongPlayer = GetComponentInParent<SongPlayer>();
            MusicIntensitySettings.OnCurrentIntensityChanged += AdjustVolumeForIntensity;
            RefreshVolume();
        }

        private void OnDisable()
        {
            SongPlayer = null;
            MusicIntensitySettings.OnCurrentIntensityChanged -= AdjustVolumeForIntensity;
        }

        private void RefreshVolume()
        {
            StopAllCoroutines();
            if (!AudioSource) return;
            float songPlayerVolume = _songPlayer
                ? _songPlayer.Volume
                : 0;
            float intensityVolume = _stem
                ? _stem.Volumes.Evaluate(MusicIntensitySettings.CurrentIntensity)
                : 0;
            AudioSource.volume = songPlayerVolume * intensityVolume;
        }

        private void AdjustVolumeForIntensity(float intensity)
        {
            StopAllCoroutines();
            if (!AudioSource) return;
            float songPlayerVolume = _songPlayer
                ? _songPlayer.Volume
                : 0;
            float intensityVolume = _stem
                ? _stem.Volumes.Evaluate(intensity)
                : 0;
            float startVolume = AudioSource.volume;
            float endVolume = songPlayerVolume * intensityVolume;
            StartCoroutine(LerpVolume(startVolume, endVolume, .5f));
        }

        private IEnumerator LerpVolume(float startVolume, float endVolume, float seconds)
        {
            for (float timer = 0f; timer < seconds; timer += Time.unscaledDeltaTime)
            {
                float progress = timer / seconds;
                AudioSource.volume = Mathf.Lerp(startVolume, endVolume, progress);
                yield return null;
            }
            AudioSource.volume = endVolume;
        }

        public void Play()
        {
            if (!AudioSource || !_stem) return;
            AudioSource.clip = _stem.GetAudioClip();
            AudioSource.Play();
            RefreshVolume();
        }

        public void Pause()
        {
            if (AudioSource) AudioSource.Pause();
        }

        public void Resume()
        {
            if (AudioSource) AudioSource.Play();
        }
        
        public void Stop()
        {
            if (AudioSource) AudioSource.Stop();
        }
    }
}
