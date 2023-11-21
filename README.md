# Adaptive Music
*Version: 1.0.0*
## Description: 
A system for playing music at various predefined intensities. 
---
## Key Scripts & Components: 
1. public class **Song** : ScriptableObject
   * Properties: 
      * public String ***DisplayName***  { get; }
      * public AudioMixerGroup ***AudioMixerGroup***  { get; }
      * public List<Stem> ***Stems***  { get; }
1. public class **SongPlayer** : MonoBehaviour
   * Actions: 
      * public event Action ***OnSongChanged*** 
      * public event Action ***OnVolumeChanged*** 
   * Properties: 
      * public Boolean ***Started***  { get; }
      * public Boolean ***IsPaused***  { get; }
      * public Song ***Song***  { get; set; }
      * public float ***Volume***  { get; }
   * Methods: 
      * public void ***RefreshStemPlayers***()
      * public void ***Play***()
      * public void ***Pause***()
      * public void ***Resume***()
      * public void ***Stop***()
      * public void ***TogglePause***()
      * public void ***FadeIn***(float fadeInSeconds, Action onComplete)
      * public void ***FadeOut***(float fadeOutSeconds, Action onComplete)
1. public static class **SongPlayersManager**
1. public class **Stem** : ScriptableObject
   * Properties: 
      * public List<AudioClip> ***AudioClips***  { get; }
      * public AnimationCurve ***Volumes***  { get; }
1. public class **StemPlayer** : MonoBehaviour
   * Properties: 
      * public AudioSource ***AudioSource***  { get; }
   * Methods: 
      * public void ***Initialize***(SongPlayer songPlayer, Stem stem, AudioMixerGroup audioMixerGroup)
      * public void ***Play***()
      * public void ***Pause***()
      * public void ***Resume***()
      * public void ***Stop***()
1. public class **VolumeEditorAttribute** : PropertyAttribute
### Intensity
1. public class **CurrentMusicIntensityText** : MonoBehaviour
1. public static class **MusicIntensitySettings**
1. public class **MusicIntensitySlider** : MonoBehaviour
