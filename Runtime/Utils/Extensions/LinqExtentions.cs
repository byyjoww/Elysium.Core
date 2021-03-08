using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elysium.Utils
{
    public static class LinqExtentions
    {
        public static T ArgMin<T>(this IEnumerable<T> list, Func<T, T, int> CompareTo)
        {
            if (list.Count() == 0)
                return default;

            T min = list.First();

            foreach (T element in list)
            {
                if (CompareTo(element, min) < 0)
                    min = element;
            }
            return min;
        }
        public static T ArgMin<T>(this IEnumerable<T> list, Func<T, int> Kernel)
        {
            return list.ArgMin((a, b) => Kernel(a) - Kernel(b));
        }
        public static T ArgMax<T>(this IEnumerable<T> list, Func<T, T, int> CompareTo)
        {
            if (list.Count() == 0)
                return default;

            T min = list.First();

            foreach (T element in list)
            {
                if (CompareTo(element, min) > 0)
                    min = element;
            }
            return min;
        }
        public static T ArgMax<T>(this IEnumerable<T> list, Func<T, int> Kernel)
        {
            return list.ArgMax((a, b) => Kernel(a) - Kernel(b));
        }

        public static T Random<T>(this IEnumerable<T> list)
        {
            List<T> l = list.ToList();
            int index = UnityEngine.Random.Range(0, l.Count);
            return l[index];
        }

        public static Dictionary<TKey, TValue> ToDictionaryUnique<TKey, TValue>(this IEnumerable<TValue> container, Func<TValue, TKey> keySelector)
        {
            Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();
            foreach (TValue s in container)
            {
                if (!dict.ContainsKey(keySelector(s)))
                {
                    dict.Add(keySelector(s), s);
                }
                else if (!dict[keySelector(s)].Equals(s))
                {
                    Debug.LogWarning("A value with [Key: " + keySelector(s) + "] is already present in dictionary. Cannot add [Value:" + s + "].");
                }
            }
            return dict;
        }

        public static bool CheckIfInCollider(Vector3 positionToTest)
        {
            Vector3 Point;
            Vector3 Start = new Vector3(0, 100, 0); // This is defined to be some arbitrary point far away from the collider.
            Vector3 Goal = positionToTest; // This is the point we want to determine whether or not is inside or outside the collider.
            Vector3 Direction = Goal - Start; // This is the direction from start to goal.
            Direction.Normalize();
            int Itterations = 0; // If we know how many times the raycast has hit faces on its way to the target and back, we can tell through logic whether or not it is inside.
            Point = Start;

            while (Point != Goal) // Try to reach the point starting from the far off point.  This will pass through faces to reach its objective.
            {
                RaycastHit hit;
                if (Physics.Linecast(Point, Goal, out hit)) // Progressively move the point forward, stopping everytime we see a new plane in the way.
                {
                    Itterations++;
                    Point = hit.point + Direction / 100.0f; // Move the Point to hit.point and push it forward just a touch to move it through the skin of the mesh (if you don't push it, it will read that same point indefinately).
                }
                else
                {
                    Point = Goal; // If there is no obstruction to our goal, then we can reach it in one step.
                }
            }
            while (Point != Start) // Try to return to where we came from, this will make sure we see all the back faces too.
            {
                RaycastHit hit;
                if (Physics.Linecast(Point, Start, out hit))
                {
                    Itterations++;
                    Point = hit.point + -Direction / 100.0f;
                }
                else
                {
                    Point = Start;
                }
            }
            if (Itterations % 2 == 0)
            {
                Debug.Log("Is currently inside a collider.");
                return true;
            }
            else if (Itterations % 2 == 1)
            {
                Debug.Log("Is currently not inside a collider.");
                return false;
            }
            else
            {
                throw new Exception("Not a valid output.");
            }
        }
    }
}