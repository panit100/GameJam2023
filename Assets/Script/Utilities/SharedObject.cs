using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Utilities
{
    public class SharedObject : Singleton<SharedObject>
    {
        private readonly Dictionary<Type, object> objects = new Dictionary<Type, object>();

        public SharedObject()
        {
        }

        public void Add<T>(T obj) where T : class
        {
            Type key = typeof(T);
            if (!objects.ContainsKey(key))
            {
                objects.Add(key, obj);
            }
            else
            {
                Debug.Log($"Object of same key detected: {key.AssemblyQualifiedName}");
            }
        }

        public void Remove<T>(T obj) where T : class
        {
            this.Remove<T>();
        }

        public void Remove<T>() where T : class
        {
            objects.Remove(typeof(T));
        }

        public T Get<T>() where T : class
        {
            Type key = typeof(T);
            objects.TryGetValue(typeof(T), out object result);
            return result as T;
        }

        public bool TryGet<T>(out T resultT) where T : class
        {
            bool result = objects.TryGetValue(typeof(T), out object resultObj);
            resultT = resultObj as T;
            return result;
        }
    }
}

