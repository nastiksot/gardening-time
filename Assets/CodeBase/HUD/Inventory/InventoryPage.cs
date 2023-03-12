using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Inventory
{
    public class InventoryPage : MonoBehaviour
    {
        [SerializeField]
        InventoryItem inventoryItemPrefab;
        const int k_MaxItemValue = 6;
        public bool IsFull => InventoryItems.Count >= k_MaxItemValue;
        public readonly List<InventoryItem> InventoryItems = new List<InventoryItem>();
        public event Action<InventoryPage> OnDestroyPage;

        public void Add(PlantConfig plantConfig, int plantCount)
        {
            InventoryItem item = Instantiate(inventoryItemPrefab, transform);
            InventoryItems.Add(item);
            item.Initialize(plantConfig, plantCount);
            item.OnDestroyItem += RemoveItem;
        }

        void RemoveItem(InventoryItem item)
        {
            InventoryItems.Remove(item);
            if (InventoryItems.Count == 0)
            {
                DestroyPage();
            }
        }

        void DestroyPage()
        {
            OnDestroyPage?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
