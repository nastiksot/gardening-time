using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Inventory
{
    public class InventoryPage : MonoBehaviour
    {
        [SerializeField] private InventoryItem inventoryItemPrefab;
        private List<InventoryItem> _inventoryItems = new List<InventoryItem>();
        private const int MaxItemValue = 6;
        
        public bool TryAdd(Sprite sprite, string itemName, int countItem)
        {
            if (_inventoryItems.Count >= MaxItemValue) return false;
            InventoryItem item = Instantiate(inventoryItemPrefab, transform);
            _inventoryItems.Add(item);
            item.Initialize(sprite, itemName, countItem);
            return true;
        }
    }
}