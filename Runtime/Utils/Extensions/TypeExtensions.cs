using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elysium.Utils
{
    public static class TypeExtensions
    {
        public static bool IsSameOrSubclass(this Type _comparer, Type _comparedTo)
        {
            return _comparer.IsSubclassOf(_comparedTo) || _comparer == _comparedTo;
        }
    }
}