using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elysium.Utils
{
    public static class MonoBehaviourExtensions
    {
        public static T GetInterface<T>(this MonoBehaviour _monoBehaviour) where T : class
        {
            return _monoBehaviour.gameObject.GetInterface<T>();
        }

        public static IEnumerable<T> GetInterfaces<T>(this MonoBehaviour _monoBehaviour) where T : class
        {
            return _monoBehaviour.gameObject.GetInterfaces<T>();
        }

        public static T GetInterfaceInChildren<T>(this MonoBehaviour _monoBehaviour) where T : class
        {
            return _monoBehaviour.gameObject.GetInterfaceInChildren<T>();
        }

        public static IEnumerable<T> GetInterfacesInChildren<T>(this MonoBehaviour _monoBehaviour) where T : class
        {
            return _monoBehaviour.gameObject.GetInterfacesInChildren<T>();
        }

        public static List<T> GetComponentsInBrothers<T>(this MonoBehaviour _monoBehaviour) where T : MonoBehaviour
        {
            if (_monoBehaviour.transform == null || _monoBehaviour.transform.parent == null)
                return new List<T>();

            List<T> components = new List<T>();
            foreach (Transform child in _monoBehaviour.transform.parent)
            {
                components.AddRange(child.GetComponents<T>());
            }
            return components;
        }

        public static T GetComponentInBrothers<T>(this MonoBehaviour _monoBehaviour) where T : MonoBehaviour
        {
            List<T> components = _monoBehaviour.GetComponentsInBrothers<T>();
            return components.Count > 0 ? components.Single() : null;
        }

        public static void ValidateComponent<T>(this MonoBehaviour _monoBehaviour, ref T _field, bool _searchInChildren = false) where T : Component
        {
            if (_field == null)
            {
                _field = _searchInChildren ? _monoBehaviour.GetComponentInChildren<T>() : _monoBehaviour.GetComponent<T>();
                Debug.Log("Auto-getting " + typeof(T) + " component from " + _monoBehaviour, _monoBehaviour);
                if (_field == null)
                    Debug.LogWarning("Auto-getter could not find component of type " + typeof(T) + " from " + _monoBehaviour, _monoBehaviour);
            }
        }
    }
}