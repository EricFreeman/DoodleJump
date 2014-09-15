using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Events
{
    public static class EventAggregator
    {
        private static Dictionary<Type, List<object>> _cache = new Dictionary<Type, List<object>>();

        /// <summary>
        /// Creates the cache for objects that listen to each event
        /// </summary>
        public static void UpdateCache<T>()
        {
            var type = typeof(IListener<T>);
            var list = new List<object>();

            // Get all types of IListener<T>
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(type))
                .Each(x =>
                {
                    // Add existing unity objects that listen for event
                    GameObject.FindObjectsOfType<MonoBehaviour>()
                    .Where(t => t.GetType() == x)
                    .Each(list.Add);
                });

            _cache[typeof(T)] = list;
        }

        public static void Register<T>(this object obj)
        {
            if(!_cache.ContainsKey(typeof(T))) _cache[typeof(T)] = new List<object>();
            _cache[typeof(T)].Add(obj);
        }

        public static void UnRegister<T>(this object obj)
        {
            if (!_cache.ContainsKey(typeof (T))) return;
            _cache[typeof (T)].Remove(obj);
        }

        public static void UpdateCacheAndSendMessage<T>(T message)
        {
            UpdateCache<T>();
            SendMessage(message);
        }

        public static void SendMessage<T>(T message)
        {
            // If there's no entires for message, try updating cache before not sending anything
            if (!_cache.ContainsKey(message.GetType()))
            {
                UpdateCache<T>();
                SendMessage(message);
                return;
            }

            // Finish sending message
            _cache[message.GetType()].Each(x => ((IListener<T>)x).Handle(message));
        }
    }
}
