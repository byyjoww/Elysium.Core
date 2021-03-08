using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elysium.Utils
{
    public static class Vector3Extensions
    {
        public static Vector3 SetX(this Vector3 _vec, float _x)
        {
            return new Vector3(_x, _vec.y, _vec.z);
        }

        public static Vector3 SetY(this Vector3 _vec, float _y)
        {
            return new Vector3(_vec.x, _y, _vec.z);
        }

        public static Vector3 SetZ(this Vector3 _vec, float _z)
        {
            return new Vector3(_vec.x, _vec.y, _z);
        }

        public static Vector3 Multiply(this Vector3 _vec, float _x, float _y, float _z)
        {
            return new Vector3(_vec.x * _x, _vec.y * _y, _vec.z * _z);
        }

        public static Vector3 Multiply(this Vector3 _vec, Vector3 _other)
        {
            return _vec.Multiply(_other.x, _other.y, _other.z);
        }

        public static Vector3 Clamp(this Vector3 _vec, Vector3 _min, Vector3 _max)
        {
            _vec.x = Mathf.Clamp(_vec.x, _min.x, _max.x);
            _vec.y = Mathf.Clamp(_vec.y, _min.y, _max.y);
            _vec.z = Mathf.Clamp(_vec.z, _min.z, _max.z);

            return _vec;
        }

        public static bool Approximately(this Vector3 _vec, Vector3 _compared, float _eps = 0)
        {
            _eps = Mathf.Max(_eps, 0) + (float)1E-20;
            return Mathf.Abs((_vec - _compared).magnitude) <= _eps;
        }
    }
}