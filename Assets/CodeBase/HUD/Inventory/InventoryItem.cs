using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Event;
using CodeBase.Infrastructure.Event.Events;
using CodeBase.Infrastructure.Services;
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

        int m_Count;

        //TODO: to think
        PlantConfig m_PlantConfig;
        IEventsService m_EventsService;
        public event Action<InventoryItem> OnDestroyItem;

        void Awake()
        {
            m_EventsService = ServiceLocator.Container.Single<IEventsService>();
        }

        void Start()
        {
            itemButton.onClick.AddListener(OnItemButtonPressed);
            m_EventsService.Subscribe<OnPlantSpawned>(OnPlantSpawned);
        }

        void OnDestroy()
        {
            itemButton.onClick.RemoveAllListeners();
            m_EventsService.Unsubscribe<OnPlantSpawned>(OnPlantSpawned);
        }

        public void Initialize(PlantConfig plantConfig, int plantCount)
        {
            m_PlantConfig = plantConfig;
            image.sprite = plantConfig.sprites[0];
            itemName.text = plantConfig.type.ToString();
            ChangeAmount(plantCount);
        }

        public PlantData UpdateProgress()
        {
            return m_PlantConfig == null ? null : new PlantData(m_PlantConfig.type, m_Count);
        }

        void OnPlantSpawned(OnPlantSpawned evt)
        {
            var plantType = evt.PlantType;
            if (plantType != m_PlantConfig.type)
            {
                return;
            }

            ChangeAmount(-1);
            if (m_Count <= 0)
            {
                DestroyItem();
            }
        }

        void DestroyItem()
        {
            OnDestroyItem?.Invoke(this);
            Destroy(gameObject);
        }

        void OnItemButtonPressed()
        {
            OnPlantSeedSelected();
        }

        void OnPlantSeedSelected()
        {
            var evt = new OnPlantSeedSelected
            {
                PlantConfig = m_PlantConfig,
            };
            m_EventsService.Post(evt);
        }

        void ChangeAmount(int plantCount)
        {
            m_Count += plantCount;
            SetCountText(m_Count);
        }

        void SetCountText(int plantCount)
        {
            count.text = plantCount.ToString();
        }
    }
}
