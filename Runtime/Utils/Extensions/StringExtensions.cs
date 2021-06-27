using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elysium.Utils
{
    public static class StringExtensions
    {
        public static string Title(this string _s)
        {
            if (string.IsNullOrEmpty(_s)) { return string.Empty; }                
            return char.ToUpper(_s[0]) + _s.Substring(1).ToLower();
        }
    }
}
