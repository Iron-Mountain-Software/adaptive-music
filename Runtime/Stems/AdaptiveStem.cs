using UnityEngine;

namespace IronMountain.AdaptiveMusic.Stems
{
    public abstract class AdaptiveStem : ScriptableObject
    {
        public const int VolumeSteps = 11;
        
        [SerializeField] [VolumeEditor] private AnimationCurve volumes;
        
        public AnimationCurve Volumes => volumes;

        public abstract AudioClip GetAudioClip();

        private void Reset()
        {
            volumes = new AnimationCurve();
            for (int level = 0; level <= VolumeSteps; level++)
            {
                float value = (float) level / VolumeSteps;
                volumes.AddKey(value, Mathf.Lerp(0, 1, value));
            }
        }
    }
}