using UnityEditor;

namespace Editor
{
    public class DotProductEditor : EditorWindow
    {
        [MenuItem("Tools/Dot Product")]
        public static void ShowWindow()
        {
            DotProductEditor window = (DotProductEditor) GetWindow(typeof(DotProductEditor), true , "DotProduct");
            window.Show();
        }
    }
}