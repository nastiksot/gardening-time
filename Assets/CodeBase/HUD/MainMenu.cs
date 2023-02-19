using CodeBase.Infrastructure.Event;
using CodeBase.Infrastructure.Event.Events;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.HUD
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        Button closeButton;
        [SerializeField]
        Button marketButton;
        [SerializeField]
        Button inventoryButton;
        [SerializeField]
        CanvasDisabler inventoryCanvasDisabler;
        [SerializeField]
        CanvasDisabler buttonHolderCanvasDisabler;
        [SerializeField]
        CanvasSwitcher canvasSwitcher;

        IEventsService m_EventsService;
        
        void Awake()
        {
            m_EventsService = ServiceLocator.Container.Single<IEventsService>();
        }

        void Start()
        {
            closeButton.onClick.AddListener(CloseInventory);
            inventoryButton.onClick.AddListener(OpenInventory);
            m_EventsService.Subscribe<OnPlantSeedSelected>(OnPlantSeedSelected);
        }

        void OnDestroy()
        {
            m_EventsService.Unsubscribe<OnPlantSeedSelected>(OnPlantSeedSelected);
        }

        void OnPlantSeedSelected(OnPlantSeedSelected _)
        {
            CloseInventory();
        }

        void OpenInventory()
        {
            canvasSwitcher.OpenTable(inventoryCanvasDisabler);
        }

        void CloseInventory()
        {
            canvasSwitcher.OpenTable(buttonHolderCanvasDisabler);
        }
    }
}