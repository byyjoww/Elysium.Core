using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elysium.Utils
{
    public static class GameObjectExtentions
    {
        public static T GetInterface<T>(this GameObject inObj) where T : class
        {
            if (!typeof(T).IsInterface)
            {
                Debug.LogError(typeof(T).ToString() + ": is not an interface!");
                return null;
            }

            return inObj.GetComponents<Component>().OfType<T>().FirstOrDefault();
        }

        public static IEnumerable<T> GetInterfaces<T>(this GameObject inObj) where T : class
        {
            if (!typeof(T).IsInterface)
            {
                Debug.LogError(typeof(T).ToString() + ": is not an interface!");
                return Enumerable.Empty<T>();
            }

            return inObj.GetComponents<Component>().OfType<T>();
        }

        public static T GetInterfaceInChildren<T>(this GameObject inObj) where T : class
        {
            if (!typeof(T).IsInterface)
            {
                Debug.LogError(typeof(T).ToString() + ": is not an interface!");
                return null;
            }

            return inObj.GetComponentsInChildren<Component>().OfType<T>().FirstOrDefault();
        }

        public static IEnumerable<T> GetInterfacesInChildren<T>(this GameObject inObj) where T : class
        {
            if (!typeof(T).IsInterface)
            {
                Debug.LogError(typeof(T).ToString() + ": is not an interface!");
                return Enumerable.Empty<T>();
            }

            return inObj.GetComponentsInChildren<Component>().OfType<T>();
        }

        public static bool IsDestroyed(this GameObject gameObject)
        {
            return gameObject == null && !ReferenceEquals(gameObject, null);
        }
    }
}