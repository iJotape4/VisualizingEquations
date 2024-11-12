using UnityEditor;
using UnityEngine;

public class CrossProductEditor : CommonEditor, IUpdateSceneGUI
{
    public Vector3 m_p;
    public Vector3 m_q;
    public Vector3 m_pxq;
    
    private SerializedObject obj;
    private SerializedProperty propP, propQ, propPxQ;
    
    private GUIStyle guiStyle = new GUIStyle();
    
    [MenuItem("Tools/Cross Product")]
    public static void ShowWindow()
    {
        CrossProductEditor window = (CrossProductEditor) GetWindow(typeof(CrossProductEditor), true , "Cross Product");
        window.Show();
    }

    private void SetDefaultValues()
    {
        m_p = new Vector3(0f, 1f, 0f);
        m_q = new Vector3(1.0f, 0.0f, 0f);
    }
    private void OnEnable()
    {
        if(m_p == Vector3.zero && m_q == Vector3.zero && m_pxq == Vector3.zero)
        {
            SetDefaultValues();
        }

        obj = new SerializedObject(this);
        propP = obj.FindProperty("m_p");
        propQ = obj.FindProperty("m_q");
        propPxQ = obj.FindProperty("m_pxq");
        
        guiStyle.fontSize = 20;
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
        DrawBlockGUI(("P"), propP);
        DrawBlockGUI(("Q"), propQ);
        DrawBlockGUI(("P x Q"), propPxQ);

        if (obj.ApplyModifiedProperties())
        {
            SceneView.RepaintAll();
        }

        if (GUILayout.Button("Reset Values"))
        {
            SetDefaultValues();
        }
    }

    public void SceneGUI(SceneView view)
    {
        Vector3 p = Handles.PositionHandle(m_p, Quaternion.identity);
        Vector3 q = Handles.PositionHandle(m_q, Quaternion.identity);

        Handles.color = Color.blue;
        Vector3 pxq = CrossProduct(p, q);
        Handles.DrawSolidDisc(pxq, Vector3.forward, 0.05f);
        
        if(m_p != p || m_q != q)
        {
            Undo.RecordObject(this, "Tool Move");
            m_p = p;
            m_q = q;
            m_pxq = pxq;
            RepaintOnGUI();
        }
        
        DrawLineGUI(p, "P", Color.green);
        DrawLineGUI(q, "Q", Color.red);
        DrawLineGUI(pxq, "P x Q", Color.blue);
    }
    
    private void DrawLineGUI ( Vector3 pos, string text, Color color)
    {
        Handles.color = color;
        Handles.Label(pos, text, guiStyle);
        Handles.DrawAAPolyLine(3f, pos, Vector3.zero);
    }

    private void RepaintOnGUI()
    {
        Repaint();
    }

    Vector3 CrossProduct(Vector3 p, Vector3 q)
    {
        float x = p.y * q.z - p.z * q.y;
        float y = p.z * q.x - p.x * q.z;
        float z = p.x * q.y - p.y * q.x;
        
        return new Vector3(x, y, z);
    }

}
