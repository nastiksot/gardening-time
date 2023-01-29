using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Inventory
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField]
        Image image;
        [SerializeField]
        TMP_Text itemName;
        [SerializeField]
        TMP_Text count;
        [SerializeField]
        Button itemButton;

        void Awake()
        {
            itemButton.onClick.AddListener(OnItemButtonPressed);
        }

        void OnItemButtonPressed()
        {
            
        }

        public void Initialize(Sprite sprite, string itemName, int countItem)
        {
            image.sprite = sprite;
            this.itemName.text = itemName;
            count.text = countItem.ToString();
        }
    }
}