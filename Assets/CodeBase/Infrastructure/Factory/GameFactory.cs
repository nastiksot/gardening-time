using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
     public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public GameObject HeroGameObject { get; private set; }

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }
  
        public void InstantiateHUD()
        {
           // GameObject hud = InstantiateRegistered(AssetPath.MenuPrefabPath);
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
 
        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject instantiate = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(instantiate);
            return instantiate;
        }

        private void RegisterProgressWatchers(GameObject hero)
        {
            foreach (ISavedProgressReader progressReader in hero.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progress)
            {
                ProgressWriters.Add(progress);
            }

            ProgressReaders.Add(progressReader);
        }
    }

    public static class AssetPath
    {
        public const string MenuPrefabPath = "Menu/Menu";
    }
}