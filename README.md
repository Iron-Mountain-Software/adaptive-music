# Adaptive Music
*Version: 1.0.3*
## Description: 
A system for playing music at various predefined intensities.
## Package Mirrors: 
[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODg3LnBuZw==/original/npRUfq.png'>](https://github.com/Iron-Mountain-Software/adaptive-music.git)[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODk4LnBuZw==/original/Rv4m96.png'>](https://iron-mountain.itch.io/adaptive-music)[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODkyLnBuZw==/original/Fq0ORM.png'>](https://www.npmjs.com/package/com.iron-mountain.adaptive-music)
---
## Key Scripts & Components: 
1. public class **Song** : ScriptableObject
   * Properties: 
      * public String ***DisplayName***  { get; }
      * public AudioMixerGroup ***AudioMixerGroup***  { get; }
      * public List<AdaptiveStem> ***Stems***  { get; }
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
1. public class **StemPlayer** : MonoBehaviour
   * Properties: 
      * public AudioSource ***AudioSource***  { get; }
   * Methods: 
      * public void ***Initialize***(SongPlayer songPlayer, AdaptiveStem stem, AudioMixerGroup audioMixerGroup)
      * public void ***Play***()
      * public void ***Pause***()
      * public void ***Resume***()
      * public void ***Stop***()
1. public class **VolumeEditorAttribute** : PropertyAttribute
### Intensity
1. public class **CurrentMusicIntensityText** : MonoBehaviour
1. public static class **MusicIntensitySettings**
1. public class **MusicIntensitySlider** : MonoBehaviour
### Stems
1. public abstract class **AdaptiveStem** : ScriptableObject
   * Properties: 
      * public AnimationCurve ***Volumes***  { get; }
   * Methods: 
      * public abstract AudioClip ***GetAudioClip***()
1. public class **BasicAdaptiveStem** : AdaptiveStem
   * Methods: 
      * public override AudioClip ***GetAudioClip***()
1. public class **RandomAdaptiveStem** : AdaptiveStem
   * Properties: 
      * public List<AudioClip> ***AudioClips***  { get; }
   * Methods: 
      * public override AudioClip ***GetAudioClip***()
