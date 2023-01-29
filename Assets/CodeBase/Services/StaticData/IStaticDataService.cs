namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService
    {
        void LoadPlants();
        PlantsConfig ForPlant(PlantType plantType);
    }
}
