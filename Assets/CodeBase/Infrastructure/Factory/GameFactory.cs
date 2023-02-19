using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.StaticData;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
    public class GameFactory : IGameFactory
    {
        readonly IAssetProvider m_Assets;
        readonly IStaticDataService m_StaticDataService;
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameFactory(IAssetProvider assets, IStaticDataService staticDataService)
        {
            m_Assets = assets;
            m_StaticDataService = staticDataService;
        }

        public void InstantiateHUD()
        {
            GameObject hud = InstantiateRegistered(AssetPath.MenuCanvasPath);
        }

        public void InstantiateGameplay()
        {
            GameObject hud = InstantiateRegistered(AssetPath.GameplayCanvasPath);
        }

        public GameObject InstantiatePlant(PlantType type, Transform parent)
        {
            GameObject plantPrefab = m_StaticDataService.ForPlant(type).prefab;
            GameObject instantiatedPlant = Object.Instantiate(plantPrefab, parent);
            
            RegisterProgressWatchers(instantiatedPlant);
            return instantiatedPlant;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        GameObject InstantiateRegistered(string prefabPath, Vector3 position)
        {
            GameObject instantiate = m_Assets.Instantiate(prefabPath, position);
            RegisterProgressWatchers(instantiate);
            return instantiate;
        }

        GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject instantiate = m_Assets.Instantiate(prefabPath);
            RegisterProgressWatchers(instantiate);
            return instantiate;
        }

        void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progress)
            {
                ProgressWriters.Add(progress);
            }

            ProgressReaders.Add(progressReader);
        }
    }
}
