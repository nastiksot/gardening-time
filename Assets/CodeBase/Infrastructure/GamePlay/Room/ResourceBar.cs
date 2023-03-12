using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Room
{
    public class ResourceBar : MonoBehaviour
    {
        [SerializeField]
        Image filledImage;

        public void SetValue(float fillValue)
        {
            filledImage.fillAmount = fillValue;
        }

        public void SetVisibility(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}
