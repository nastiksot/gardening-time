using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.HUD.InteractiveUI
{
    public class LampUI : MonoBehaviour
    {
        [SerializeField]
        Toggle lamp;
        [SerializeField]
        Image frogImage;
        [SerializeField]
        Sprite[] frogSprites;

        void Awake()
        {
            lamp.onValueChanged.AddListener(SwitchLight);
            SwitchLight(lamp.isOn);
        }

        void SwitchLight(bool isOn)
        {
            frogImage.sprite = isOn ? frogSprites[1] : frogSprites[0];
        }
    }
}
