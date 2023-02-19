using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    public static class EditorTools
    {
        [MenuItem("Tools/Clear Player Preferences")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
