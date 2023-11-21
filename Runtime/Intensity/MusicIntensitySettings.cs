using System;
using UnityEngine;

namespace IronMountain.AdaptiveMusic.Intensity
{
    public static class MusicIntensitySettings
    {
        public static event Action<float> OnCurrentIntensityChanged;

        public const float MinimumIntensityLevel = 0;
        public const float MaximumIntensityLevel = 1;

        private static float _currentIntensity = MaximumIntensityLevel;

        public static float CurrentIntensity
        {
            get => _currentIntensity;
            set
            {
                value = Mathf.Clamp(value, MinimumIntensityLevel, MaximumIntensityLevel);
                if (_currentIntensity == value) return;
                _currentIntensity = value;
                OnCurrentIntensityChanged?.Invoke(_currentIntensity);
            }
        }
    }
}