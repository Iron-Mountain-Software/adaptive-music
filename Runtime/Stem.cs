using System.Collections.Generic;
using IronMountain.AdaptiveMusic.Intensity;
using UnityEngine;

namespace IronMountain.AdaptiveMusic
{
    public class Stem : ScriptableObject
    {
        [SerializeField] private List<AudioClip> audioClips = new ();
        [SerializeField] [VolumeEditor] private AnimationCurve volumes;
        
        public List<AudioClip> AudioClips => audioClips;
        public AnimationCurve Volumes => volumes;

        private void Reset()
        {
            volumes = new AnimationCurve();
            for (int level = 0; level <= MusicIntensitySettings.MaximumIntensityLevel; level++)
            {
                float value = (float) level / MusicIntensitySettings.MaximumIntensityLevel;
                volumes.AddKey(value, Mathf.Lerp(0, 1, value));
            }
        }

#if UNITY_EDITOR
        
        private void OnValidate()
        {
            RefreshName();
            CorrectVolumes();
        }
        
        [ContextMenu("Refresh Name")]
        private void RefreshName()
        {
           name = AudioClips.Count > 0 && AudioClips[0]
                ? AudioClips[0].name
                : name;
        }

        [ContextMenu("Correct Volumes")]
        private void CorrectVolumes()
        {
            while (volumes.keys.Length > MusicIntensitySettings.MaximumIntensityLevel + 1)
                volumes.RemoveKey(volumes.keys.Length - 1);
            float[] values = new float[volumes.keys.Length];
            for (int i = 0; i < volumes.keys.Length; i++)
                values[i] = volumes.keys[i].value;
            while (volumes.keys.Length > 0)
                volumes.RemoveKey(0);
            for (int i = 0; i < values.Length; i++)
                volumes.AddKey(new Keyframe(
                    (float) i / MusicIntensitySettings.MaximumIntensityLevel,
                    Mathf.Clamp01(values[i]))
                );
            while (volumes.keys.Length < MusicIntensitySettings.MaximumIntensityLevel + 1)
                volumes.AddKey(new Keyframe(
                    (float) volumes.keys.Length / MusicIntensitySettings.MaximumIntensityLevel, 
                    0)
                );
        }
        
#endif
        
    }
}
