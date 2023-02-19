using StansAssets.Foundation.Patterns;

namespace CodeBase.Infrastructure.Event.Events
{
    public class OnPlantSpawned : IEvent
    {
       public PlantType PlantType { get; set; }
    }
}
