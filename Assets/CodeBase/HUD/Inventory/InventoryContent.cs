using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Inventory
{
    public class InventoryContent : CanvasScroll<InventoryPage>, ISavedProgress
    {
        [SerializeField]
        Button leftArrowButton;
        [SerializeField]
        Button rightArrowButton;
        [SerializeField]
        RectTransform inventoryContainer;
        [SerializeField]
        InventoryPage inventoryPagePrefab;
  
        ISaveLoadService m_SaveLoadService;
        IStaticDataService m_StaticDataService;

        void Awake()
        {
            m_SaveLoadService = ServiceLocator.Container.Single<ISaveLoadService>();
            m_StaticDataService = ServiceLocator.Container.Single<IStaticDataService>();
            InstantiateInventoryPage();
            SubscribeOnScrollButtons();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            List<PlantData> inventoryPlants = progress.state.inventoryPlants;
            for (var i = 0; i < inventoryPlants.Count; i++)
            {
                PlantsConfig plantsConfig = m_StaticDataService.ForPlant(inventoryPlants[i].type);
                if (plantsConfig == null) continue;
                for (var j = 0; j < scrollContentList.Count;)
                {
                    InventoryPage inventoryPage = scrollContentList[j];
                    Sprite plantSprite = plantsConfig.sprites[0];
                    var plantName = inventoryPlants[i].type.ToString();
                    int plantCount = inventoryPlants[i].count;

                    if (inventoryPage.TryAdd(plantSprite, plantName, plantCount)) break;

                    InventoryPage newInventoryPage = InstantiateInventoryPage();
                    newInventoryPage.TryAdd(plantSprite, plantName, plantCount);
                    break;
                }
            }
        }

        public void UpdateProgress(PlayerProgress progress) { }

        void SubscribeOnScrollButtons()
        {
            rightArrowButton.onClick.AddListener(() => ScrollToPage(ScrollSide.Right, inventoryContainer));
            leftArrowButton.onClick.AddListener(() => ScrollToPage(ScrollSide.Left, inventoryContainer));
        }

        InventoryPage InstantiateInventoryPage()
        {
            InventoryPage inventoryPage = Instantiate(inventoryPagePrefab, transform);
            scrollContentList.Add(inventoryPage);
            return inventoryPage;
        }
    }
}
