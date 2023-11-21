using UnityEngine;
using UnityEngine.UI;

namespace IronMountain.AdaptiveMusic.Intensity
{
    [ExecuteAlways]
    [RequireComponent(typeof(Slider))]
    public class MusicIntensitySlider : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        private void Awake()
        {
            if (!slider) slider = GetComponent<Slider>();
        }

        private void OnValidate()
        {
            if (!slider) slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            InitializeSlider();
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnDisable()
        {
            slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        private void InitializeSlider()
        {
            if (!slider) return;
            slider.minValue = MusicIntensitySettings.MinimumIntensityLevel;
            slider.maxValue = MusicIntensitySettings.MaximumIntensityLevel;
            slider.value = MusicIntensitySettings.CurrentIntensity;
        }

        private void OnSliderValueChanged(float value)
        {
            MusicIntensitySettings.CurrentIntensity = value;
        }
    }
}
