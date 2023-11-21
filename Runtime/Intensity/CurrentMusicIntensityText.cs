using System;
using UnityEngine;
using UnityEngine.UI;

namespace IronMountain.AdaptiveMusic.Intensity
{
    [ExecuteAlways]
    [RequireComponent(typeof(Text))]
    public class CurrentMusicIntensityText : MonoBehaviour
    {
        [SerializeField] private Text text;

        private void Awake()
        {
            if (!text) text = GetComponent<Text>();
        }

        private void OnValidate()
        {
            if (!text) text = GetComponent<Text>();
        }

        private void OnEnable()
        {
            MusicIntensitySettings.OnCurrentIntensityChanged += Refresh;
            Refresh(MusicIntensitySettings.CurrentIntensity);
        }

        private void OnDisable()
        {
            MusicIntensitySettings.OnCurrentIntensityChanged -= Refresh;
        }

        private void Refresh(float intensity)
        {
            if (!text) return;
            text.text = intensity.ToString("P");
        }
    }
}
