using IronMountain.AdaptiveMusic.Stems;
using UnityEditor;
using UnityEngine;

namespace IronMountain.AdaptiveMusic.Editor
{
    [CustomPropertyDrawer(typeof(VolumeEditorAttribute))]
    public class VolumeEditorPropertyDrawer : PropertyDrawer
    {
        private float _labelHeight = 15;
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 100;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.CurveField(new Rect(position.x, position.y, position.width, position.height - _labelHeight), property, Color.cyan, new Rect(0, 0, 1, 1), GUIContent.none);
            EditorGUI.EndDisabledGroup();
            AnimationCurve curve = property.animationCurveValue;
            float paddingLeft = 0;
            float paddingRight = 9;
            float width = (position.width - paddingRight) / (curve.keys.Length - 1);
            float height = position.height - _labelHeight;
            for (int i = 0; i < curve.keys.Length; i++)
            {
                float time = (float) i / (AdaptiveStem.VolumeSteps - 1);
                float value = curve.keys[i].value;
                value = GUI.VerticalSlider(new Rect(position.x + paddingLeft + width * i, position.y, width, height), value, 1, 0);
                curve.MoveKey(i, new Keyframe(time, value));
            }
            float labelY = position.y + height;
            EditorGUI.LabelField(new Rect(position.x, labelY, 30, _labelHeight), "Min");
            EditorGUI.LabelField(new Rect(position.x + position.width - 30, labelY, 30, _labelHeight), "Max");
            property.animationCurveValue = curve;
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}