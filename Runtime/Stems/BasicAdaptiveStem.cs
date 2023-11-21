using UnityEngine;

namespace IronMountain.AdaptiveMusic.Stems
{
    public class BasicAdaptiveStem : AdaptiveStem
    {
        [SerializeField] private AudioClip audioClip;

        public override AudioClip GetAudioClip() => audioClip;

#if UNITY_EDITOR


        private void OnValidate()
        {
            RefreshName();
        }
        
        [ContextMenu("Refresh Name")]
        private void RefreshName()
        {
           name = audioClip ? audioClip.name : name;
        }

#endif
        
    }
}
