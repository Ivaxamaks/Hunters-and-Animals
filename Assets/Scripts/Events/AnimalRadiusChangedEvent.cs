using SimpleEventBus.Events;

namespace Events
{
    public class AnimalRadiusChangedEvent : EventBase
    {
        public float Radius { get; }
        
        public AnimalRadiusChangedEvent(float radius)
        {
            Radius = radius;
        }
    }
}