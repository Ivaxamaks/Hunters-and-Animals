using SimpleEventBus.Interfaces;
namespace Plugins.SimpleEventBus

{
    public static class EventStreams
    {
        public static IEventBus UserInterface { get; } = new EventBus();
    }
}

