using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Inventory
{
    public class InventoryContent : CanvasScroll<InventoryPage>, ISavedProgress
    {
        [SerializeField] private Button leftArrowButton;
        [SerializeField] private Button rightArrowButton;
        [SerializeField] private RectTransform inventoryContainer;
        [SerializeField] private InventoryPage inventoryPagePrefab;
        [SerializeField] private PlantsConfig[] _plantsConfigs;

        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _saveLoadService = ServiceLocator.Container.Single<ISaveLoadService>();
            InstantiateInventoryPage();
            SubscribeOnScrollButtons();
        }

        private InventoryPage InstantiateInventoryPage()
        {
            InventoryPage inventoryPage = Instantiate(inventoryPagePrefab, transform);
            scrollContentList.Add(inventoryPage);
            return inventoryPage;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            List<Plant> inventoryPlants = progress.state.inventoryPlants;
            for (var i = 0; i < inventoryPlants.Count; i++)
            {
                PlantsConfig plantsConfig = _plantsConfigs.FirstOrDefault(x => x.Type == inventoryPlants[i].type);
                if (plantsConfig == null) continue;
                foreach (InventoryPage inventoryPage in scrollContentList)
                {
                    Sprite plantSprite = plantsConfig.Sprites[0];
                    var plantName = inventoryPlants[i].type.ToString();
                    int plantCount = inventoryPlants[i].count;

                    if (inventoryPage.TryAdd(plantSprite, plantName, plantCount)) break;

                    InventoryPage newInventoryPage = InstantiateInventoryPage();
                    newInventoryPage.TryAdd(plantSprite, plantName, plantCount);
                    break;
                }
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
        }

        private void SubscribeOnScrollButtons()
        {
            rightArrowButton.onClick.AddListener(() => ScrollToPage(ScrollSide.Right, inventoryContainer));
            leftArrowButton.onClick.AddListener(() => ScrollToPage(ScrollSide.Left, inventoryContainer));
        }
    }
}