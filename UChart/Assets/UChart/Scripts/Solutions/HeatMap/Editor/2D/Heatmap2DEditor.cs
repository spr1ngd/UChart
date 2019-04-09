
using UnityEditor;
using UnityEditor.UI;

namespace UChart.HeatMap.Editor
{
    [CustomEditor(typeof(Heatmap2D),true)]
    [CanEditMultipleObjects]
    public class Heatmap2DEditor : ImageEditor
    {
        private SerializedProperty m_lineColor;
        private SerializedProperty m_textureWidth;
        private SerializedProperty m_textureHeight;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_lineColor = serializedObject.FindProperty("lineColor");
            m_textureWidth = serializedObject.FindProperty("width");
            m_textureHeight = serializedObject.FindProperty("height");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            RaycastControlsGUI();
            TypeGUI();
            EditorGUILayout.PropertyField(m_lineColor);
            EditorGUILayout.PropertyField(m_textureWidth);
            EditorGUILayout.PropertyField(m_textureHeight);
            serializedObject.ApplyModifiedProperties();
        }
    }
}