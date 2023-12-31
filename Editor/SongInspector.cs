using System.Collections.Generic;
using IronMountain.AdaptiveMusic.Stems;
using UnityEditor;
using UnityEngine;

namespace IronMountain.AdaptiveMusic.Editor
{
    [CustomEditor(typeof(Song), true)]
    public class SongInspector : StyledInspector
    {
        private Song _song;
        private SerializedObject _serializedSong;
        private readonly Dictionary<AdaptiveStem, UnityEditor.Editor> _cachedEditors = new ();

        private static AdaptiveStem AddStemToSong<T>(Song song) where T : AdaptiveStem
        {
            if (!song) return null;
            T stem = CreateInstance(typeof(T)) as T;
            if (!stem || string.IsNullOrEmpty(AssetDatabase.GetAssetPath(song))) return null;
            AssetDatabase.AddObjectToAsset(stem, song);
            AssetDatabase.SaveAssets();
            return stem;
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            if (!_song && target) _song = (Song) target;
        }

        public override void OnInspectorGUI()
        {
            if (!_song && target) _song = (Song) target;
            DrawInformation();
            DrawAddStemButton();
            DrawStems();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawInformation()
        {
            EditorGUILayout.BeginVertical(Container);
            string label = string.IsNullOrWhiteSpace(_song.DisplayName) ? _song.name : _song.DisplayName;
            GUILayout.Label(label, H1);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("displayName"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("audioMixerGroup"));
            EditorGUILayout.EndVertical();
        }

        private void DrawAddStemButton()
        {
            GUILayout.Space(6);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Basic Stem", GUILayout.Height(25)))
            {
                _song.Stems.Add(AddStemToSong<BasicAdaptiveStem>(_song));
            }
            if (GUILayout.Button("Add Random Stem", GUILayout.Height(25)))
            {
                _song.Stems.Add(AddStemToSong<RandomAdaptiveStem>(_song));
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawStems()
        {
            EditorGUILayout.BeginVertical();
            foreach (AdaptiveStem stem in _song.Stems)
            {
                if (!stem) continue;
                UnityEditor.Editor cachedEditor = _cachedEditors.ContainsKey(stem)
                    ? _cachedEditors[stem] : null;
                CreateCachedEditor(stem, null, ref cachedEditor);
                cachedEditor.OnInspectorGUI();
                if (!_cachedEditors.ContainsKey(stem)) _cachedEditors.Add(stem, cachedEditor);
            }
            EditorGUILayout.EndVertical();
        }
    }
}
