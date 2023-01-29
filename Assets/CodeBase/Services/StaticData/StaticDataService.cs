using System.Collections.Generic;
using System.Linq;
using CodeBase.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        Dictionary<PlantType, PlantsConfig> m_PlantsConfigs = new Dictionary<PlantType, PlantsConfig>();

        public void LoadPlants() => 
            m_PlantsConfigs = Resources.LoadAll<PlantsConfig>(AssetPath.PlantsStaticDataPath)
                .ToDictionary(x => x.type);

        public PlantsConfig ForPlant(PlantType plantType) => 
            m_PlantsConfigs.TryGetValue(plantType, out PlantsConfig plantsConfig) 
                ? plantsConfig 
                : null;
    }
}