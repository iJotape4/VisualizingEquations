using UnityEngine;
using UnityEditor;

namespace Editor
{
    public class DotProductEditor : EditorWindow
    {
        public Vector3 m_P0, m_P1, m_c;
        private SerializedObject obj;
        private SerializedProperty propP0, propP1, propC;
        
        [MenuItem("Tools/Dot Product")]
        public static void ShowWindow()
        {
            DotProductEditor window = (DotProductEditor) GetWindow(typeof(DotProductEditor), true , "DotProduct");
            window.Show();
        }

        private void OnEnable()
        {
            if(m_P0 == Vector3.zero && m_P1 == Vector3.zero && m_c == Vector3.zero)
            {
                m_P0 = new Vector3(0f, 1f, 0f);
                m_P1 = new Vector3(0.5f, 0.5f, 0f);
                m_c = Vector3.zero;
            }
            SceneView.duringSceneGui += SceneGUI;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= SceneGUI;
        }
        
        private void OnGUI()
        {
        }

        private void SceneGUI(SceneView scene)
        {
            
        }
    }
}