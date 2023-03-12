using StansAssets.Foundation.Patterns;

namespace CodeBase.Infrastructure.Event.Events
{
    public class OnPlantSeedSelected : IEvent
    {
        public PlantConfig PlantConfig { get; set; }
    }
}
