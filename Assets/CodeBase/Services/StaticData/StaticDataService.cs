using System.Collections.Generic;
using System.Linq;
using CodeBase.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        Dictionary<PlantType, PlantConfig> m_PlantsConfigs = new Dictionary<PlantType, PlantConfig>();
        public PictureConfig[] PictureConfigs { get; private set; }

        public void LoadAll()
        {
            LoadPlants();
            LoadPictures();
        }

        public void LoadPlants() =>
            m_PlantsConfigs = Resources.LoadAll<PlantConfig>(AssetPath.PlantsStaticDataPath)
                .ToDictionary(x => x.type);

        public void LoadPictures() =>
            PictureConfigs = Resources.LoadAll<PictureConfig>(AssetPath.PictureStaticDataPath)
                .ToArray();

        public PlantConfig ForPlant(PlantType plantType) =>
            m_PlantsConfigs.TryGetValue(plantType, out PlantConfig plantsConfig)
                ? plantsConfig
                : null;
    }
}
