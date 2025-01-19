using System;
using System.Collections.Generic;

namespace ObserverPattern
{
    public abstract class SubjectBase
    {
        protected Dictionary<EventEnum, Action> _eventActionDict = new();

        public void AddEventActions(Dictionary<EventEnum, Action> eventActionDict)
        {
            if (eventActionDict == null) return;

            foreach (var kvp in eventActionDict)
            {
                if (_eventActionDict.ContainsKey(kvp.Key) && _eventActionDict[kvp.Key] != kvp.Value)
                {
                    _eventActionDict[kvp.Key] += kvp.Value;
                }
                else
                {
                    _eventActionDict.Add(kvp.Key, kvp.Value);
                }
            }
        }

        public void Subscribe() => Observer.OnEvent += OnEvent;
        public void Unsubscribe() => Observer.OnEvent -= OnEvent;
    
        protected virtual void OnEvent(EventEnum eventEnum)
        {
            if (_eventActionDict.TryGetValue(eventEnum, out var action)) action?.Invoke();
        }
    }

    public class ObserverSubject : SubjectBase
    {
        public ObserverSubject(Dictionary<EventEnum, Action> eventActionDict, bool autoSubscribe = false)
        {
            AddEventActions(eventActionDict);

            if (autoSubscribe) Subscribe();
        }

        ~ObserverSubject()
        {
            Unsubscribe();
        }
    }
}