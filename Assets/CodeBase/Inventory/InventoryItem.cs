using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Inventory
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text count;

        public void Initialize(Sprite sprite, string itemName, int countItem)
        {
            image.sprite = sprite;
            this.itemName.text = itemName;
            count.text = countItem.ToString();
        }
    }
}