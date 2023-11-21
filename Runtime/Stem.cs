using System.Collections.Generic;
using IronMountain.AdaptiveMusic.Intensity;
using UnityEngine;

namespace IronMountain.AdaptiveMusic
{
    public class Stem : ScriptableObject
    {
        public const int VolumeSteps = 10;
        
        [SerializeField] private List<AudioClip> audioClips = new ();
        [SerializeField] [VolumeEditor] private AnimationCurve volumes;
        
        public List<AudioClip> AudioClips => audioClips;
        public AnimationCurve Volumes => volumes;

        private void Reset()
        {
            volumes = new AnimationCurve();
            for (int level = 0; level <= VolumeSteps; level++)
            {
                float value = (float) level / VolumeSteps;
                volumes.AddKey(value, Mathf.Lerp(0, 1, value));
            }
        }

#if UNITY_EDITOR
        
        private void OnValidate()
        {
            RefreshName();
        }
        
        [ContextMenu("Refresh Name")]
        private void RefreshName()
        {
           name = AudioClips.Count > 0 && AudioClips[0]
                ? AudioClips[0].name
                : name;
        }

#endif
        
    }
}
