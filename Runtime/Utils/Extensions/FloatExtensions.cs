using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elysium.Utils
{
    public static class FloatExtensions
    {
        public static float Remap(this float _f, float _fromMin, float _fromMax, float _toMin, float _toMax)
        {
            float t = (_f - _fromMin) / (_fromMax - _fromMin);
            return Mathf.LerpUnclamped(_toMin, _toMax, t);
        }

        public static bool IsNullOrEmpty(this float[] _array)
        {
            return _array == null || _array.Length == 0;
        }
    }
}