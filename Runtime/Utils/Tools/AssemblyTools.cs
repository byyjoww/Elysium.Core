using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Elysium.Utils
{
    public static class AssemblyTools
    {
        public static T[] GetAllScriptableObjects<T>() where T : ScriptableObject
        {
#if UNITY_EDITOR
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
            T[] a = new T[guids.Length];
            for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return a;
#endif
            throw new Exception("Script not callable from outside Unity Editor!");
        }

        public static IEnumerable<Type> GetSubclasses<T>()
        {
            return typeof(T).GetSubclasses();
        }
        public static IEnumerable<Type> GetSubclasses(this Type inheritingFrom)
        {
            Type[] allTypesInAssembly = Assembly.GetAssembly(inheritingFrom).GetTypes();
            // Filter assembly classes: get subclasses of type and remove abstract classes
            return allTypesInAssembly.Where(type => type.IsClass && type.IsSubclassOf(inheritingFrom) && !type.IsAbstract);
        }

        public static MethodInfo GetMethod<TReturn>(Type type)
        {
            // Get the properties of the class but include inherited properties.
            List<PropertyInfo> found = (new Type[] { type }).Concat(type.GetInterfaces()).SelectMany(i => i.GetProperties()).
                Where(m => m.PropertyType == typeof(TReturn)).ToList();

            if (found.Count == 1)
            {
                return found.Single().GetMethod;
            }
            else
            {
                Debug.LogError("Type [" + type + "] contains " + found.Count + " Methods / Properties of return type [" + typeof(TReturn).Name + "].");
                return null;
            }
        }
        /**
         * Creates a dictionary from a list of types, mapping a singular enum to a class, using reflection.
         */
        public static Dictionary<En, Type> BuildDictionaryFromEnumProperty<En>(IEnumerable<Type> types) where En : Enum
        {
            var keyValuePairs = new Dictionary<En, Type>();

            foreach (Type type in types)
            {
                MethodInfo property = GetMethod<En>(type);

                if (property != null)
                {
                    En key = (En)property.Invoke(Activator.CreateInstance(type), null);

                    if (!keyValuePairs.ContainsKey(key))
                    {
                        keyValuePairs.Add(key, type);
                    }
                    else if (!keyValuePairs[key].Equals(type))
                    {
                        throw new Exception("Trying to add duplicated entries to dictionary: [Class " + keyValuePairs[key] + " and " + type + "] both have Enum [" + key + "].");
                    }
                }
            }

            return keyValuePairs;
        }

        public static string TypeToString(Type t)
        {
            return t.AssemblyQualifiedName;
        }
        public static Type StringToType(string s)
        {
            return Type.GetType(s);
        }
    }
}