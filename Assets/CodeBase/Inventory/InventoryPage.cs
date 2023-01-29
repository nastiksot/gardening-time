using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Inventory
{
    public class InventoryPage : MonoBehaviour
    {
        [SerializeField] private InventoryItem inventoryItemPrefab;
        private List<InventoryItem> _inventoryItems = new List<InventoryItem>();
        private const int MaxItemValue = 6;
        
        public bool TryAdd(PlantsConfig plantsConfig, int itemCount)
        {
            if (_inventoryItems.Count < MaxItemValue) return false;
            InventoryItem item = Instantiate(inventoryItemPrefab);
            item.Initialize(plantsConfig, itemCount);
            return true;
        }
    }
}