using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IronMountain.AdaptiveMusic
{
    public class SongPlayer : MonoBehaviour
    {
        public event Action OnSongChanged;
        public event Action OnVolumeChanged;

        [SerializeField] private Song song;
        [SerializeField] private bool dontDestroyOnLoad;
        [SerializeField] private bool playOnStart = true;
        [SerializeField] [Range(0, 1)] private float volume;

        [Header("Cache")]
        private Song _loadedSong;
        private readonly List<StemPlayer> _stemPlayers = new ();
        
        public bool Started { get; private set; }
        public bool IsPaused { get; private set; }

        public Song Song
        {
            get => song;
            set
            {
                if (song == value) return;
                song = value;
                RefreshStemPlayers();
                OnSongChanged?.Invoke();
            }
        }
        
        public float Volume
        {
            get => volume;
            private set
            {
                value = Mathf.Clamp01(value);
                if (volume == value) return;
                volume = value;
                OnVolumeChanged?.Invoke();
            }
        }

        private void Awake()
        {
            if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            SongPlayersManager.Register(this);
            Stop();
        }

        private void OnDisable()
        {
            SongPlayersManager.Unregister(this);
            Stop();
        }

        private void Start()
        {
            if (playOnStart) Play();
        }

        public void RefreshStemPlayers()
        {
            DestroyAllStemPlayers();
            DestroyAllChildren();
            CreateStemPlayers();
        }
        
        private void DestroyAllChildren()
        {
            foreach (Transform child in transform)
            {
                if (!child) continue;
                if (Application.isPlaying) Destroy(child.gameObject);
                else DestroyImmediate(child.gameObject);
            }
        }

        private void DestroyAllStemPlayers()
        {
            foreach (StemPlayer stemPlayer in _stemPlayers)
            {
                if (!stemPlayer) continue;
                if (Application.isPlaying) Destroy(stemPlayer.gameObject);
                else DestroyImmediate(stemPlayer.gameObject);
            }
            _stemPlayers.Clear();
            _loadedSong = null;
        }

        private void CreateStemPlayers()
        {
            if (!Song) return;
            foreach (Stem stem in Song.Stems)
            {
                if (!stem) continue;
                GameObject instantiatedObject = new GameObject(stem.name, typeof(StemPlayer));
                instantiatedObject.transform.parent = transform;
                StemPlayer stemPlayer = instantiatedObject.GetComponent<StemPlayer>();
                stemPlayer.Initialize(this, stem, Song.AudioMixerGroup);
                _stemPlayers.Add(stemPlayer);
            }
            _loadedSong = Song;
        }

        public void Play()
        {
            RefreshStemPlayers();
            foreach (StemPlayer stemPlayer in _stemPlayers)
            {
                if (stemPlayer) stemPlayer.Play();
            }
            Started = true;
            IsPaused = false;
        }

        public void Pause()
        {
            IsPaused = true;
            foreach (StemPlayer stemPlayer in _stemPlayers)
            {
                if (stemPlayer) stemPlayer.Pause();
            }
        }
        
        public void Resume()
        {
            IsPaused = false;
            foreach (StemPlayer stemPlayer in _stemPlayers)
            {
                if (stemPlayer) stemPlayer.Resume();
            }
        }

        public void Stop()
        {
            Started = false;
            IsPaused = false;
            foreach (StemPlayer stemPlayer in _stemPlayers)
            {
                if (stemPlayer) stemPlayer.Stop();
            }
        }

        public void TogglePause()
        {
            IsPaused = !IsPaused;
            if (IsPaused) Pause();
            else Resume();
        }

        private void Update()
        {
            if (!Started || IsPaused) return;
            foreach (StemPlayer stemPlayer in _stemPlayers)
            {
                if (stemPlayer && stemPlayer.AudioSource.isPlaying) return;
            }
            foreach (StemPlayer stemPlayer in _stemPlayers)
            {
                if (stemPlayer) stemPlayer.Play();
            }
        }

        public void FadeIn(float fadeInSeconds = 1, Action onComplete = null)
        {
            StopAllCoroutines();
            StartCoroutine(FadeVolume(0, 1, fadeInSeconds, onComplete));
        }
        
        public void FadeOut(float fadeOutSeconds = 1, Action onComplete = null)
        {
            StopAllCoroutines();
            StartCoroutine(FadeVolume(1, 0, fadeOutSeconds, onComplete));
        }

        private IEnumerator FadeVolume(float startVolume, float endVolume, float seconds, Action onComplete)
        {
            float progress = Mathf.InverseLerp(startVolume, endVolume, Volume);
            for (float timer = seconds * progress; timer < seconds; timer += Time.unscaledDeltaTime)
            {
                progress = timer / seconds;
                Volume = Mathf.Lerp(startVolume, endVolume, progress);
                yield return null;
            }
            Volume = endVolume;
            onComplete?.Invoke();
        }

        private void OnValidate()
        {
            OnVolumeChanged?.Invoke();
        }
    }
}