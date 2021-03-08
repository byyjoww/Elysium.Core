using System.Linq;
using UnityEngine;

namespace Elysium.Utils
{
    [System.Serializable]
    public class Database<T> where T : ScriptableObject
    {
        public T[] Elements => elements;
        [SerializeField] private T[] elements;

        public T GetElementByName(string _name)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i].name == _name)
                {
                    // Debug.Log($"OnLoad: element matches {elements[i].name} => {_name}");
                    return elements[i];
                }
            }

            return null;
        }

        public void Refresh()
        {
            elements = AssemblyTools.GetAllScriptableObjects<T>();
        }
    }

    [System.Serializable]
    public class IDatabase<T> where T : class
    {
        public ScriptableObject[] Elements => elements;
        [SerializeField] private ScriptableObject[] elements;

        public T GetElementByName(string _name)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i].name == _name)
                {
                    // Debug.Log($"OnLoad: element matches {elements[i].name} => {_name}");
                    T t = elements[i] as T;
                    return t;
                }
            }

            return default(T);
        }

        public void Refresh()
        {
            elements = AssemblyTools.GetAllScriptableObjects<ScriptableObject>().Where(x => x is T).Distinct().ToArray();
        }
    }
}
