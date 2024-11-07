using UnityEngine;
using UnityEditor;

namespace Editor
{
    public class DotProductEditor : EditorWindow
    {
        public Vector3 m_P0, m_P1, m_c;
        private SerializedObject obj;
        private SerializedProperty propP0, propP1, propC;
        private GUIStyle guiStyle = new GUIStyle();
        
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

            obj = new SerializedObject(this);
            propP0 = obj.FindProperty("m_P0");
            propP1 = obj.FindProperty("m_P1");
            propC = obj.FindProperty("m_c");

            guiStyle.fontSize = 25;
            guiStyle.fontStyle = FontStyle.Bold;
            guiStyle.normal.textColor = Color.white;
            
            SceneView.duringSceneGui += SceneGUI;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= SceneGUI;
        }
        
        private void OnGUI()
        {
            obj.Update();
            
            DrawBlockGUI("P0", propP0);
            DrawBlockGUI("P1", propP1);
            DrawBlockGUI("C", propC);

            if (obj.ApplyModifiedProperties())
            {
                SceneView.RepaintAll(); 
            }
        }

        private void DrawBlockGUI(string lab, SerializedProperty prop)
        {
            EditorGUILayout.BeginHorizontal(" box");
            EditorGUILayout.LabelField(lab, GUILayout.Width(50));
            EditorGUILayout.PropertyField(prop, GUIContent.none);
            EditorGUILayout.EndHorizontal();
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
            
            DrawLabel(p0, p1, c);
        }

        Vector3 SetMovePoint(Vector3 pos)
        {
            float size = HandleUtility.GetHandleSize(Vector3.zero);
            return Handles.FreeMoveHandle(pos , Quaternion.identity, size, Vector3.zero, Handles.SphereHandleCap);
        }
        
        float Dotproduct(Vector3 p0, Vector3 p1, Vector3 c)
        {
            Vector3 a = (p0 - c).normalized;
            Vector3 b = (p1 - c).normalized;
            return Vector3.Dot(a, b);
        }

        void DrawLabel(Vector3 p0, Vector3 p1, Vector3 c)
        {
            Handles.Label(c, Dotproduct(p0, p1, c).ToString("F1"), guiStyle);
            Handles.color = Color.black;
            
            Handles.DrawAAPolyLine(3f, p0, c);
            Handles.DrawAAPolyLine(3f, p1, c);
        }
    }
}