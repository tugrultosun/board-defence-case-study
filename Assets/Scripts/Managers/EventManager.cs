using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Managers
{
    public class EventManager : Singleton<EventManager>
    {
        public delegate void EventHandlerDelegate(object eventData);

        private Dictionary<Type, List<EventHandlerDelegate>> _listeners = new();

        private void AddListener(Type eventType, EventHandlerDelegate eventHandler)
        {
            if (_listeners.TryGetValue(eventType, out List<EventHandlerDelegate> eventListeners))
            {
                if (!eventListeners.Contains(eventHandler)) // make sure we dont add duplicate
                {
                    eventListeners.Add(eventHandler);
                }
            }
            else
            {
                eventListeners = new List<EventHandlerDelegate>() { eventHandler };
                _listeners.Add(eventType, eventListeners);
            }
        }

        private void RemoveListener(Type eventType, EventHandlerDelegate eventHandler)
        {
            if (_listeners.TryGetValue(eventType, out List<EventHandlerDelegate> eventListeners))
            {
                eventListeners.Remove(eventHandler);
            }
        }

        private void TriggerEvent(Type eventType, object eventData = null)
        {
            if (_listeners.TryGetValue(eventType, out List<EventHandlerDelegate> eventListeners))
            {
                foreach (var listener in eventListeners.ToList())
                {
                    try // ensure all the listents are called
                    {
                        listener.Invoke(eventData);
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
            }
        }

        public void AddListener<T>(EventHandlerDelegate eventHandler)
        {
            AddListener(typeof(T), eventHandler);
        }

        public void RemoveListener<T>(EventHandlerDelegate eventHandler)
        {
            RemoveListener(typeof(T), eventHandler);
        }

        public void TriggerEvent<T>(object eventData = null)
        {
            TriggerEvent(typeof(T), eventData);
        }
    }
}
