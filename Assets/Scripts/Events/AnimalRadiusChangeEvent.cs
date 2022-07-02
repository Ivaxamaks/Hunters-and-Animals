using SimpleEventBus.Events;

namespace Events
{
    public class AnimalRadiusChangeEvent : EventBase
    {
        public float Radius { get; }
        
        public AnimalRadiusChangeEvent(float radius)
        {
            Radius = radius;
        }
    }
}