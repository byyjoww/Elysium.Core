using UnityEngine;

namespace Elysium.Utils
{
    public static class MathExtensions
    {
        public static float Modulus(float _a, float _b)
        {
            return _a - _b * Mathf.Floor(_a / _b);
        }
    }
}