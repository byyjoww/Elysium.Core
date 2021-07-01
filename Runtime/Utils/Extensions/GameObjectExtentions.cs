using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elysium.Utils
{
    public static class GameObjectExtentions
    {
        public static T GetInterface<T>(this GameObject _gameObject) where T : class
        {
            if (!typeof(T).IsInterface)
            {
                Debug.LogError(typeof(T).ToString() + ": is not an interface!");
                return null;
            }

            return _gameObject.GetComponents<Component>().OfType<T>().FirstOrDefault();
        }

        public static IEnumerable<T> GetInterfaces<T>(this GameObject _gameObject) where T : class
        {
            if (!typeof(T).IsInterface)
            {
                Debug.LogError(typeof(T).ToString() + ": is not an interface!");
                return Enumerable.Empty<T>();
            }

            return _gameObject.GetComponents<Component>().OfType<T>();
        }

        public static T GetInterfaceInChildren<T>(this GameObject _gameObject) where T : class
        {
            if (!typeof(T).IsInterface)
            {
                Debug.LogError(typeof(T).ToString() + ": is not an interface!");
                return null;
            }

            return _gameObject.GetComponentsInChildren<Component>().OfType<T>().FirstOrDefault();
        }

        public static IEnumerable<T> GetInterfacesInChildren<T>(this GameObject _gameObject) where T : class
        {
            if (!typeof(T).IsInterface)
            {
                Debug.LogError(typeof(T).ToString() + ": is not an interface!");
                return Enumerable.Empty<T>();
            }

            return _gameObject.GetComponentsInChildren<Component>().OfType<T>();
        }

        public static bool IsDestroyed(this GameObject _gameObject)
        {
            return _gameObject == null && !ReferenceEquals(_gameObject, null);
        }

        public static T AddComponentIfNull<T>(this GameObject _gameObject) where T : UnityEngine.Component
        {
            T component = _gameObject.GetComponent<T>();
            if (component == null)
            {
                component = _gameObject.AddComponent<T>();
            }
            return component;
        }
    }
}