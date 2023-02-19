using StansAssets.Foundation.Patterns;

namespace CodeBase.Infrastructure.Event.Events
{
    public class OnPlantSeedSelected : IEvent
    {
        public PlantsConfig PlantsConfig { get; set; }
    }
}
