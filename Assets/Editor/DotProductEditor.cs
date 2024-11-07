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
            Handles.color = Color.red;
            Vector3 p0 = SetMovePoint(m_P0);
            
            Handles.color = Color.green;
            Vector3 p1 = SetMovePoint(m_P1);
            
            Handles.color = Color.blue;
            Vector3 c = SetMovePoint(m_c);
            
            if(m_P0 != p0 || m_P1 != p1 || m_c != c)
            {
                m_P0 = p0;
                m_P1 = p1;
                m_c = c;
                Repaint();
            }
        }

        Vector3 SetMovePoint(Vector3 pos)
        {
            float size = HandleUtility.GetHandleSize(Vector3.zero);
            return Handles.FreeMoveHandle(pos , Quaternion.identity, size, Vector3.zero, Handles.SphereHandleCap);
        }
    }
}