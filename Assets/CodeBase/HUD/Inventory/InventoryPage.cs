using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Inventory
{
    public class InventoryPage : MonoBehaviour
    {
        [SerializeField]
        InventoryItem inventoryItemPrefab;
        readonly List<InventoryItem> m_InventoryItems = new List<InventoryItem>();
        const int k_MaxItemValue = 6;
        
        public bool TryAdd(Sprite sprite, string itemName, int countItem)
        {
            if (m_InventoryItems.Count >= k_MaxItemValue) return false;
            InventoryItem item = Instantiate(inventoryItemPrefab, transform);
            m_InventoryItems.Add(item);
            item.Initialize(sprite, itemName, countItem);
            return true;
        }
    }
}