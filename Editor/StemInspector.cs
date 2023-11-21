using IronMountain.AdaptiveMusic.Stems;
using UnityEditor;
using UnityEngine;

namespace IronMountain.AdaptiveMusic.Editor
{
    [CustomEditor(typeof(AdaptiveStem), true)]
    public class StemInspector : StyledInspector
    {
        private AdaptiveStem _stem;
        private bool _expanded;
        
        private readonly Rect _defaultVolumeBounds = new (0, 0, 1, 1);

        protected override void OnEnable()
        {
            base.OnEnable();
            if (!_stem && target) _stem = (AdaptiveStem) target;
        }

        public override void OnInspectorGUI()
        {
            if (!_stem && target) _stem = (AdaptiveStem) target;
            
            GUILayout.Space(6);
            GUILayout.BeginVertical(Container);
            GUILayout.BeginHorizontal();
            
            string label = _stem.name;
            
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
                DrawPropertiesExcluding(serializedObject, "m_Script", "volumes");
                EditorGUI.indentLevel--;
            }
            else
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.CurveField(serializedObject.FindProperty("volumes"), Color.cyan, _defaultVolumeBounds, GUIContent.none, GUILayout.Height(40));
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