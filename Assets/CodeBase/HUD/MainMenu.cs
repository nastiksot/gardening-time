using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.HUD
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        Button closeButton;
        [SerializeField]
        Button marketButton;
        [SerializeField]
        Button inventoryButton;
        [SerializeField]
        CanvasDisabler inventoryCanvasDisabler;
        [SerializeField]
        CanvasDisabler buttonHolderCanvasDisabler;
        [SerializeField]
        CanvasSwitcher canvasSwitcher;

        void Awake()
        {
            closeButton.onClick.AddListener(CloseInventory);
            inventoryButton.onClick.AddListener(OpenInventory);
        }

        void OpenInventory()
        {
            canvasSwitcher.OpenTable(inventoryCanvasDisabler);
        }

        void CloseInventory()
        {
            canvasSwitcher.OpenTable(buttonHolderCanvasDisabler);
        }
    }
}