using UnityEditor;
using UnityEngine;

public class CrossProductEditor : CommonEditor, IUpdateSceneGUI
{
    public Vector3 m_p;
    public Vector3 m_q;
    public Vector3 m_pxq;
    
    private SerializedObject obj;
    private SerializedProperty propP, propQ, propPxQ;
    
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
        
        SceneView.duringSceneGui += SceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= SceneGUI;
    }

    private void OnGUI()
    {
        
    }

    public void SceneGUI(SceneView view)
    {
        
    }
}
