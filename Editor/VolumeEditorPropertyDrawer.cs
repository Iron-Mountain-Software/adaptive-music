using UnityEditor;
using UnityEngine;

namespace IronMountain.AdaptiveMusic.Editor
{
    [CustomPropertyDrawer(typeof(VolumeEditorAttribute))]
    public class VolumeEditorPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 100;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.CurveField(position, property, Color.cyan, new Rect(0, 0, 1, 1), GUIContent.none);
            EditorGUI.EndDisabledGroup();
            float paddingLeft = 10;
            float paddingRight = 15;
            float width = (position.width - paddingRight) / 10f;
            float height = position.height;
            AnimationCurve curve = property.animationCurveValue;
            for (int i = 0; i < curve.keys.Length; i++)
            {
                float value = curve.keys[i].value;
                value = GUI.VerticalSlider(new Rect(position.x + paddingLeft + width * i, position.y, width, height), value, 1, 0);
                curve.MoveKey(i, new Keyframe(property.animationCurveValue.keys[i].time, value));
            }
            property.animationCurveValue = curve;
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}