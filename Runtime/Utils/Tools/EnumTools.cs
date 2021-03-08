using System;
using System.Collections.Generic;
using System.Linq;

namespace Elysium.Utils
{
    public static class EnumTools
    {
        public static T[] GetAllEnums<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        }

        public static T[] GetAllEnumsExcept<T>(T[] _exceptions)
        {
            return GetAllEnums<T>().Except(_exceptions).ToArray();
        }        
    }
}