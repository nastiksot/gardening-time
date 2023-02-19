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
        Image lampImage;
        [SerializeField]
        Sprite[] frogSprites;
        [SerializeField]
        Sprite[] lampSprites;

        void Awake()
        {
            lamp.onValueChanged.AddListener(SwitchLight);
            SwitchLight(lamp.isOn);
        }

        void SwitchLight(bool isOn)
        {
            lampImage.sprite = isOn ? lampSprites[1] : lampSprites[0];
            frogImage.sprite = isOn ? frogSprites[1] : frogSprites[0];
        }
    }
}
