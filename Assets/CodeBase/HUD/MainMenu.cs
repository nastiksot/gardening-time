using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.HUD
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private Button marketButton;
        [SerializeField] private Button inventoryButton;
        [SerializeField] private CanvasDisabler inventoryCanvasDisabler;
        [SerializeField] private CanvasDisabler buttonHolderCanvasDisabler;
        [SerializeField] private CanvasSwitcher canvasSwitcher;

        private void Awake()
        {
            closeButton.onClick.AddListener(CloseInventory);
            inventoryButton.onClick.AddListener(OpenInventory);
        }

        private void OpenInventory()
        {
            buttonHolderCanvasDisabler.DisplayObject(false);
            canvasSwitcher.OpenTable(inventoryCanvasDisabler);
        }

        private void CloseInventory()
        {
            canvasSwitcher.OpenTable(buttonHolderCanvasDisabler);
            buttonHolderCanvasDisabler.DisplayObject(true);
        }
    }
}