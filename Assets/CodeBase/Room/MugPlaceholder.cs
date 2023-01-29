using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Inventory;
using CodeBase.Services.SaveLoad;
using CodeBase.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Room
{
    public class MugPlaceholder : MonoBehaviour, ISavedProgress
    {
        [SerializeField]
        Button placeholderButton;
        [SerializeField]
        bool isPlaced;
        [SerializeField]
        PlantsConfig plantsConfig;
        [SerializeField]
        string guid;

        ISaveLoadService m_SaveLoadService;
        IGameFactory m_GameFactory;

        void Awake()
        {
            guid = GetComponent<UniqueId>().guid;
            m_SaveLoadService = ServiceLocator.Container.Single<ISaveLoadService>();
            m_GameFactory = ServiceLocator.Container.Single<IGameFactory>();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            foreach (MugsData mugsData in progress.mugsData)
            {
                if (mugsData.guid != guid)
                    continue;
                Spawn(mugsData.plantData.type);
                isPlaced = true;
                break;
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.mugsData.Add(new MugsData
            {
                guid = guid,
                plantData = new PlantData(type: plantsConfig.type, count: 1)
            });
        }

        public void Spawn(PlantType plantType)
        {
            m_GameFactory.InstantiatePlant(plantType, transform);
        }
    }
}
