using UnityEngine;

namespace Core
{
    public class FactoryUiView
    {
        public T Create<T>(T prefab, Transform parent = null) where T : Object
        {
            return Object.Instantiate(prefab, parent);
        }
    }
}