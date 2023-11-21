using UnityEditor;
using UnityEngine;

namespace IronMountain.AdaptiveMusic.Editor
{
    [CustomEditor(typeof(SongPlayer), true)]
    public class SongPlayerInspector : UnityEditor.Editor
    {
        private SongPlayer _songPlayer;

        private void OnEnable()
        {
            _songPlayer = (SongPlayer) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.BeginHorizontal();
            
            EditorGUI.BeginDisabledGroup(!_songPlayer || _songPlayer.Started && !_songPlayer.IsPaused);
            if (GUILayout.Button(EditorGUIUtility.IconContent("PlayButton On@2x"), GUILayout.Height(30)))
            {
                if (_songPlayer.Started && _songPlayer.IsPaused) _songPlayer.Resume();
                else _songPlayer.Play();
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(!_songPlayer || !_songPlayer.Started || _songPlayer.IsPaused);
            if (GUILayout.Button(EditorGUIUtility.IconContent("PauseButton On@2x"), GUILayout.Height(30))) _songPlayer.Pause();
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(!_songPlayer || !_songPlayer.Started);
            if (GUILayout.Button(EditorGUIUtility.IconContent("d_PreMatQuad@2x"), GUILayout.Height(30))) _songPlayer.Stop();
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndHorizontal();
        }
    }
}