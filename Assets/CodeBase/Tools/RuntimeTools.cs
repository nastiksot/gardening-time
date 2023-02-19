using UnityEngine;

namespace CodeBase.Tools.Editor
{
    public class RuntimeTools : MonoBehaviour
    {
        public void Clear()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
