using StansAssets.Foundation.Patterns;

namespace CodeBase.Infrastructure.Event.Events
{
    public class OnMugPlaceholderSelected : IEvent
    {
        public string MugGuid { get; set; }
    }
}
