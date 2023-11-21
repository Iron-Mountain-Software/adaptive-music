using System.Collections.Generic;
using UnityEngine;

namespace IronMountain.AdaptiveMusic.Stems
{
    public class RandomAdaptiveStem : AdaptiveStem
    {
        [SerializeField] private List<AudioClip> audioClips = new ();
        
        public List<AudioClip> AudioClips => audioClips;

        public override AudioClip GetAudioClip()
        {
            return audioClips?[Random.Range(0, audioClips.Count)];
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