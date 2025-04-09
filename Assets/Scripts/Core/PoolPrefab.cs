using System;
using System.Collections.Generic;
using System.Linq;
using CommonUI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    public class PoolPrefab
    {
        private readonly Dictionary<Type, ViewUi> _pool;
        
        public PoolPrefab(ViewUi[] prefabs)
        {
            _pool = prefabs.ToDictionary(k => k.GetType(), v => v);
        }

        public T GetPrefab<T>() where T : ViewUi
        {
            return _pool[typeof(T)] as T;
        }
    }
}