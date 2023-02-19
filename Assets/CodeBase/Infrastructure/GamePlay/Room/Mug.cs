using System.Linq;
using CodeBase.Data;
using CodeBase.Infrastructure.Event;
using CodeBase.Infrastructure.Event.Events;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.SaveLoad;
using CodeBase.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Room
{
    public class Mug : MonoBehaviour, ISavedProgress
    {
        [SerializeField]
        Button placeholderButton;
        [SerializeField]
        bool isPlaced;

        string m_GUID;

        IGameFactory m_GameFactory;
        IEventsService m_EventsService;
        Plant m_Plant;
        public bool IsPlaced => isPlaced;
        public string GUID => m_GUID;

        void Awake()
        {
            m_GUID = GetComponent<UniqueId>().guid;
            m_GameFactory = ServiceLocator.Container.Single<IGameFactory>();
            m_EventsService = ServiceLocator.Container.Single<IEventsService>();
        }

        void Start()
        {
            placeholderButton.onClick.AddListener(OnPlaceholderClick);
            m_EventsService.Subscribe<OnPlantSeedSelected>(OnPlantSelected);
        }

        void OnDestroy()
        {
            placeholderButton.onClick.RemoveAllListeners();
            m_EventsService.Unsubscribe<OnPlantSeedSelected>(OnPlantSelected);
        }

        void OnPlantSelected(OnPlantSeedSelected obj)
        {
            if (isPlaced)
            {
                return;
            }

            SetInteractable(true);
        }

        void OnPlaceholderClick()
        {
            OnMugPlaceholderSelected();
        }

        void OnMugPlaceholderSelected()
        {
            var evt = new OnMugPlaceholderSelected
            {
                MugGuid = m_GUID,
            };
            m_EventsService.Post(evt);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            foreach (MugsData mugsData in progress.mugsData)
            {
                if (mugsData.guid != m_GUID)
                    continue;

                Spawn(mugsData.plantData.type);
                break;
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (m_Plant == null || m_Plant.PlantType == PlantType.None)
            {
                return;
            }

            var data = progress.mugsData.FirstOrDefault(x => x.guid == m_GUID);
            if (data == null)
            {
                progress.mugsData.Add(new MugsData
                {
                    guid = m_GUID,
                    plantData = new PlantData(type: m_Plant.PlantType, count: 1)
                });
            }
        }

        public void Spawn(PlantType plantType)
        {
            if (plantType == PlantType.None)
            {
                return;
            }

            Plant plant = m_GameFactory.InstantiatePlant(plantType, transform).GetComponent<Plant>();
            m_Plant = plant;
            plant.Initialize(plantType);
            SetInteractable(false);
            isPlaced = true;
        }

        public void SetInteractable(bool state)
        {
            placeholderButton.interactable = state;
        }
    }
}
