using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Utilities
{
    public abstract class Singleton
    {
        protected static readonly List<Singleton> singletonList = new List<Singleton>();

        public static void ClearAllSingleton()
        {
            for (int i = 0; i < singletonList.Count; i++)
            {
                Singleton singleton = singletonList[i];
                singleton.Clear();
            }

            singletonList.Clear();
        }

        protected abstract void Clear();
    }

    public abstract class Singleton<T> : Singleton where T : Singleton<T>, new()
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                    singletonList.Add(instance);
                }
                return instance;
            }
        }

        public static bool HasInstance { get { return instance != null; } }

        protected override void Clear()
        {
            instance = null;
        }
    }
}
