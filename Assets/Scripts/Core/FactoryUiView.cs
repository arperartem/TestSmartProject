using CommonUI;
using UnityEngine;

namespace Core
{
    public class FactoryUiView
    {
        private readonly PoolPrefab _poolPrefab;

        public FactoryUiView(PoolPrefab poolPrefab)
        {
            _poolPrefab = poolPrefab;
        }
        
        public T Create<T>(Transform parent = null) where T : ViewUi
        {
            var prefab = _poolPrefab.GetPrefab<T>();
            return Object.Instantiate(prefab, parent);
        }
    }
}