using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace IronMountain.AdaptiveMusic
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Gameplay/Audio/Music/Song")]
    public class Song : ScriptableObject
    {
        [SerializeField] private string displayName;
        [SerializeField] private AudioMixerGroup audioMixerGroup;
        [SerializeField] private List<Stem> stems = new ();
        
        public string DisplayName => displayName;
        public AudioMixerGroup AudioMixerGroup => audioMixerGroup;
        public List<Stem> Stems => stems;

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