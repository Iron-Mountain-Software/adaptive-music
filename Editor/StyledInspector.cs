using UnityEngine;

namespace IronMountain.AdaptiveMusic.Editor
{
    public class StyledInspector : UnityEditor.Editor
    {
        protected GUIStyle Container;
        protected GUIStyle H1;
        protected GUIStyle H2;
        
        protected virtual void OnEnable()
        {
            Texture2D containerTexture = new Texture2D(1, 1);
            containerTexture.SetPixel(0, 0, new Color(0.16f, 0.16f, 0.16f));
            containerTexture.Apply();
            Container = new GUIStyle
            {
                padding = new RectOffset(7, 7, 7, 7),
                normal = new GUIStyleState
                {
                    background = containerTexture
                }
            };

            H1 = new GUIStyle
            {
                fontSize = 20,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft,
                normal = new GUIStyleState
                {
                    textColor = new Color(0.8f, 0.8f, 0.8f)
                }
            };
            
            H2 = new GUIStyle
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft,
                normal = new GUIStyleState
                {
                    textColor = new Color(0.65f, 0.65f, 0.65f)
                }
            };
        }
    }
}