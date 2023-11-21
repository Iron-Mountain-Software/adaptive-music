using UnityEditor;
using UnityEngine;

namespace IronMountain.AdaptiveMusic.Editor
{
    [CustomEditor(typeof(Stem), true)]
    public class StemInspector : StyledInspector
    {
        private Stem _stem;
        private bool _expanded;
        
        private readonly Rect _defaultVolumeBounds = new (0, 0, 1, 1);

        protected override void OnEnable()
        {
            base.OnEnable();
            if (!_stem && target) _stem = (Stem) target;
        }

        public override void OnInspectorGUI()
        {
            if (!_stem && target) _stem = (Stem) target;
            
            GUILayout.Space(6);
            GUILayout.BeginVertical(Container);
            GUILayout.BeginHorizontal();

            SerializedProperty audioClips = serializedObject.FindProperty("audioClips");

            string label = _stem.name;
            if (audioClips.arraySize == 0) label += " (No AudioClips!)";
            else if (audioClips.arraySize > 1) label += " (" + audioClips.arraySize + ")";
            
            GUILayout.Label(label, H2);
            if (GUILayout.Button("Edit", GUILayout.Width(50)))
            {
                _expanded = !_expanded;
            }
            if (GUILayout.Button(EditorGUIUtility.IconContent("TreeEditor.Trash"), GUILayout.Width(25)))
            {
                Destroy();
                return;
            }
            GUILayout.EndHorizontal();
            
            GUILayout.Space(4);
            
            if (_expanded)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("volumes"));
                GUILayout.Space(4);
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(audioClips);
                EditorGUI.indentLevel--;
            }
            else
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.CurveField(serializedObject.FindProperty("volumes"), Color.cyan, _defaultVolumeBounds, GUIContent.none);
                for (int i = 0; i < audioClips.arraySize; i++)
                {
                    EditorGUILayout.ObjectField(audioClips.GetArrayElementAtIndex(i), GUIContent.none);
                }
                EditorGUI.EndDisabledGroup();
            }
            GUILayout.EndVertical();
            
            serializedObject.ApplyModifiedProperties();
        }
        
        private void Destroy()
        {
            if (!_stem || string.IsNullOrEmpty(AssetDatabase.GetAssetPath(_stem))) return;
            AssetDatabase.RemoveObjectFromAsset(_stem);
            DestroyImmediate(_stem);
            AssetDatabase.SaveAssets();
        }
    }
}