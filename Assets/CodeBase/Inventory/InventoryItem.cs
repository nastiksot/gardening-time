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

        public void Initialize(PlantsConfig plantsConfig, int countItem)
        {
            image.sprite = plantsConfig.Sprites[0];
            itemName.text = plantsConfig.name;
            count.name = countItem.ToString();
        }
    }
}