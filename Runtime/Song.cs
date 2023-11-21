using System.Collections.Generic;
using System.Linq;
using IronMountain.AdaptiveMusic.Stems;
using UnityEngine;
using UnityEngine.Audio;

namespace IronMountain.AdaptiveMusic
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Adaptive Music/Song")]
    public class Song : ScriptableObject
    {
        [SerializeField] private string displayName;
        [SerializeField] private AudioMixerGroup audioMixerGroup;
        [SerializeField] private List<Stems.AdaptiveStem> stems = new ();
        
        public string DisplayName => displayName;
        public AudioMixerGroup AudioMixerGroup => audioMixerGroup;
        public List<Stems.AdaptiveStem> Stems => stems;

#if UNITY_EDITOR

        private void OnValidate()
        {
            PruneStems();
        }

        [ContextMenu("Prune Stems")]
        private void PruneStems()
        {
            stems = stems.Distinct().ToList();
            stems.RemoveAll(stem => !stem);
        }

#endif
        
    }
}