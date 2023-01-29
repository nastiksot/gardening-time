using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Inventory
{
    public class InventoryContent : CanvasScroll<InventoryPage>
    {
        [SerializeField] private Button leftArrowButton;
        [SerializeField] private Button rightArrowButton;
        [SerializeField] private RectTransform inventoryContainer;
        [SerializeField] private InventoryPage inventoryPagePrefab;
        
        
    }
}