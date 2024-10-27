using UnityEngine.Events;

namespace Events
{
    public static class TimeEvents 
    {
        public static UnityAction CountdownTimer;
        public static UnityAction<bool> Pause;
    }
}