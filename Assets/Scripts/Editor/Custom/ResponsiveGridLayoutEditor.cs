
using Game.Helper;
using UnityEditor;

namespace Editor.Custom
{
    [CustomEditor(typeof(ResponsiveGridLayout), true)]
    [CanEditMultipleObjects]
    public class ResponsiveGridLayoutEditor : UnityEditor.Editor
    {
        #region Variables
        
        SerializedProperty m_StartCorner;
        SerializedProperty m_StartAxis;
        SerializedProperty m_ChildAlignment;
        SerializedProperty m_ConstraintCount;
        SerializedProperty m_ReferenceSprite;
        SerializedProperty m_ElementsSpacing;

        #endregion

        #region Methods

        protected virtual void OnEnable()
        {
            m_StartCorner = serializedObject.FindProperty("m_StartCorner");
            m_StartAxis = serializedObject.FindProperty("m_StartAxis");
            m_ChildAlignment = serializedObject.FindProperty("m_ChildAlignment");
            m_ConstraintCount = serializedObject.FindProperty("m_ConstraintCount");
            m_ReferenceSprite = serializedObject.FindProperty("m_ReferenceSprite");
            m_ElementsSpacing = serializedObject.FindProperty("m_ElementsSpacing");
        }
 
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(m_StartCorner, true);
            EditorGUILayout.PropertyField(m_StartAxis, true);
            EditorGUILayout.PropertyField(m_ChildAlignment, true);
            EditorGUILayout.PropertyField(m_ConstraintCount, true); 
            EditorGUILayout.PropertyField(m_ElementsSpacing, true);
            EditorGUILayout.PropertyField(m_ReferenceSprite, true);
            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}