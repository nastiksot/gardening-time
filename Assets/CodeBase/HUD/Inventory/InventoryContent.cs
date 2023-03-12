using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.Infrastructure.Event;
using CodeBase.Infrastructure.Event.Events;
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
        IEventsService m_EventService;

        void Awake()
        {
            m_SaveLoadService = ServiceLocator.Container.Single<ISaveLoadService>();
            m_StaticDataService = ServiceLocator.Container.Single<IStaticDataService>();
            m_EventService = ServiceLocator.Container.Single<IEventsService>();
            InstantiateInventoryPage();
        }

        void Start()
        {
            m_EventService.Subscribe<OnPlantSeedSelected>(OnPlantSeedSelected);
            SubscribeOnScrollButtons();
        }

        void OnDestroy()
        {
            m_EventService.Unsubscribe<OnPlantSeedSelected>(OnPlantSeedSelected);
            UnsubscribeOnScrollButtons();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            List<PlantData> inventoryPlants = progress.state.inventoryPlants;
            for (var i = 0; i < inventoryPlants.Count; i++)
            {
                PlantConfig plantConfig = m_StaticDataService.ForPlant(inventoryPlants[i].type);
                if (plantConfig == null) continue;
                int plantCount = inventoryPlants[i].count;

                List<InventoryPage> emptyPages = new List<InventoryPage>();
                for (var j = 0; j < scrollContentList.Count; j++)
                {
                    if (scrollContentList[j].IsFull)
                    {
                        continue;
                    }

                    emptyPages.Add(scrollContentList[j]);
                }

                InventoryPage inventoryPage = emptyPages.Count == 0 ? InstantiateInventoryPage() : emptyPages.First();
                inventoryPage.Add(plantConfig, plantCount);
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            List<PlantData> plantData = new List<PlantData>();
            for (var i = 0; i < scrollContentList.Count; i++)
            {
                List<InventoryItem> inventoryItems = scrollContentList[i].InventoryItems;
                for (var j = 0; j < inventoryItems.Count; j++)
                {
                    var newData = inventoryItems[j].UpdateProgress();
                    if (newData != null)
                    {
                        plantData.Add(newData);
                    }
                }
            }

            progress.state.inventoryPlants = new List<PlantData>(plantData);
            plantData.Clear();
        }

        InventoryPage InstantiateInventoryPage()
        {
            InventoryPage inventoryPage = Instantiate(inventoryPagePrefab, transform);
            scrollContentList.Add(inventoryPage);
            inventoryPage.OnDestroyPage += RemovePage;
            return inventoryPage;
        }

        void RemovePage(InventoryPage page)
        {
            scrollContentList.Remove(page);
        }

        void OnPlantSeedSelected(OnPlantSeedSelected _)
        {
            currentPageIndex = 0;
        }

        void SubscribeOnScrollButtons()
        {
            rightArrowButton.onClick.AddListener(() => ScrollToPage(ScrollSide.Right, inventoryContainer));
            leftArrowButton.onClick.AddListener(() => ScrollToPage(ScrollSide.Left, inventoryContainer));
        }

        void UnsubscribeOnScrollButtons()
        {
            rightArrowButton.onClick.RemoveAllListeners();
            leftArrowButton.onClick.RemoveAllListeners();
        }
    }
}
