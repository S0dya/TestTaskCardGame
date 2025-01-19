
public enum EventEnum
{

}

namespace ObserverPattern
{
    public static class Observer
    {
        public delegate void EventHandler(EventEnum eventEnum);
        public static event EventHandler OnEvent;

        public static void OnHandleEvent(EventEnum eventEnum)
        {
            DebugManager.Log(DebugCategory.Observer, eventEnum.ToString());

            OnEvent?.Invoke(eventEnum);
        }
    }
}
