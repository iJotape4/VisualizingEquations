using UnityEditor;
using UnityEngine;

public class CrossProductEditor : EditorWindow
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
}
