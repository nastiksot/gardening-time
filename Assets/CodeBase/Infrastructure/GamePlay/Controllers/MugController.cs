using CodeBase.Infrastructure.Event;
using CodeBase.Infrastructure.Event.Events;
using CodeBase.Infrastructure.Services;
using CodeBase.Room;
using CodeBase.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.GamePlay.Controllers
{
    public class MugController : MonoBehaviour
    {
        [SerializeField]
        Mug[] mugPlaceholders;

        IEventsService m_EventsService;
        ISaveLoadService m_SaveLoadService;
        PlantConfig m_CurrentPlantConfig;

        void Awake()
        {
            m_EventsService = ServiceLocator.Container.Single<IEventsService>();
            m_SaveLoadService = ServiceLocator.Container.Single<ISaveLoadService>();
        }

        void Start()
        {
            m_EventsService.Subscribe<OnMugPlaceholderSelected>(OnMugSelected);
            m_EventsService.Subscribe<OnPlantSeedSelected>(OnPlantSelected);
        }

        void OnMugSelected(OnMugPlaceholderSelected evt)
        {
            if (m_CurrentPlantConfig == null)
            {
                return;
            }

            string guid = evt.MugGuid;
            for (var i = 0; i < mugPlaceholders.Length; i++)
            {
                if (mugPlaceholders[i].GUID == guid)
                {
                    mugPlaceholders[i].Spawn(m_CurrentPlantConfig.type);
                    OnPlantSpawned();
                    m_CurrentPlantConfig = null;
                    m_SaveLoadService.SaveProgress();
                    return;
                }
            }
        }

        void OnPlantSelected(OnPlantSeedSelected evt)
        {
            m_CurrentPlantConfig = evt.PlantConfig;
            //TODO: initialize cancel button 
        }

        void OnPlantSpawned()
        {
            var evt = new OnPlantSpawned
            {
                PlantType = m_CurrentPlantConfig.type
            };
            m_EventsService.Post(evt);
        }
    }
}
