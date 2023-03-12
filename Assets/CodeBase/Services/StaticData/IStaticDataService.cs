namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService
    {
        PictureConfig[] PictureConfigs { get; }
        PlantConfig ForPlant(PlantType plantType);
    }
}
