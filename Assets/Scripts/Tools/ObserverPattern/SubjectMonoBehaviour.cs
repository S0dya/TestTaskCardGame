using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public abstract class SubjectMonoBehaviour : MonoBehaviour
    {
        private SubjectBase _subjectBase = new ObserverSubject(new Dictionary<EventEnum, Action>());

        protected void AddEventActions(Dictionary<EventEnum, Action> eventActionDict) => _subjectBase.AddEventActions(eventActionDict);

        protected virtual void OnEnable() => _subjectBase.Subscribe();
        protected virtual void OnDisable() => _subjectBase.Unsubscribe();
    }
}