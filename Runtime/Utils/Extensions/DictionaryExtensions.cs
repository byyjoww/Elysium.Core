using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elysium.Utils
{
    public static class DictionaryExtensions
    {
        public static bool TryGetString(this Dictionary<string, object> _dictionary, string _key, out string _value, string _default = default)
        {
            _value = _default;
            if (_dictionary.TryGetValue(_key, out object obj))
            {
                _value = (string)obj;
            }
            return _value != default;
        }

        public static bool TryGetBool(this Dictionary<string, object> _dictionary, string _key, out bool _value, bool _default = default)
        {
            _value = _default;
            if (_dictionary.TryGetValue(_key, out object obj))
            {
                _value = Convert.ToBoolean(obj);
            }
            return _value != default;
        }

        public static bool TryGetInt(this Dictionary<string, object> _dictionary, string _key, out int _value, int _default = default)
        {
            _value = _default;
            if (_dictionary.TryGetValue(_key, out object obj))
            {
                _value = Convert.ToInt32(obj);
            }
            return _value != default;
        }

        public static bool TryGetFloat(this Dictionary<string, object> _dictionary, string _key, out float _value, float _default = default)
        {
            _value = _default;
            if (_dictionary.TryGetValue(_key, out object obj))
            {
                _value = Convert.ToSingle(obj);
            }
            return _value != default;
        }

        public static bool TryGetPrefab(this Dictionary<string, object> _dictionary, string _key, out GameObject _value, GameObject _default = null)
        {
            _value = _default;
            if (_dictionary.TryGetValue(_key, out object obj))
            {
                _value = Resources.Load<GameObject>((string)obj);
            }
            return _value != default;
        }

        public static bool TryGetMaterial(this Dictionary<string, object> _dictionary, string _key, out Material _value, Material _default = null)
        {
            _value = _default;
            if (_dictionary.TryGetValue(_key, out object obj))
            {
                _value = Resources.Load<Material>((string)obj);
            }
            return _value != default;
        }

        public static bool TryGetPosition(this Dictionary<string, object> _dictionary, string _key, out Vector3? _value, Vector3? _default = null)
        {
            _value = _default;
            if (_dictionary.TryGetValue(_key, out object obj))
            {
                string vector = (string)obj;
                if (vector.StartsWith("(") && vector.EndsWith(")"))
                {
                    vector = vector.Substring(1, vector.Length - 2);
                }
                string[] sArray = vector.Split(',');
                if (float.TryParse(sArray[0], out float f1) && float.TryParse(sArray[1], out float f2) && float.TryParse(sArray[2], out float f3))
                {
                    _value = new Vector3(f1, f2, f3);
                }
            }
            return _value != default;
        }

        public static bool TryGetRotation(this Dictionary<string, object> _dictionary, string _key, out Quaternion? _value, Quaternion? _default = null)
        {
            _value = _default;
            if (_dictionary.TryGetValue(_key, out object obj))
            {
                string quaternion = (string)obj;
                if (quaternion.StartsWith("(") && quaternion.EndsWith(")"))
                {
                    quaternion = quaternion.Substring(1, quaternion.Length - 2);
                }
                string[] sArray = quaternion.Split(',');
                if (float.TryParse(sArray[0], out float f1) && float.TryParse(sArray[1], out float f2) && float.TryParse(sArray[2], out float f3) && float.TryParse(sArray[3], out float f4))
                {
                    _value = new Quaternion(f1, f2, f3, f4);
                }
            }
            return _value != default;
        }
    }
}
