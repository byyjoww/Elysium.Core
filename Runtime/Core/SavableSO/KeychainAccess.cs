using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

namespace Elysium.Core
{
    public static class KeychainAccess
    {
#if UNITY_IPHONE && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern string _getKeychainString();
	
	[DllImport("__Internal")]
	private static extern void _saveKeychainString(string value);
#endif

        public static string GetKeychainString()
        {
            var prefsString = PlayerPrefs.GetString("unityplugin.keychain", string.Empty);
#if UNITY_IPHONE && !UNITY_EDITOR
        var keychainString = Application.platform == RuntimePlatform.IPhonePlayer ? _getKeychainString() : string.Empty;
        return string.IsNullOrEmpty(keychainString) ? prefsString : keychainString;
#else
            return prefsString;
#endif

        }

        public static void SaveKeychainString(string value)
        {
#if UNITY_IPHONE && !UNITY_EDITOR
		_saveKeychainString(value);
#endif
            PlayerPrefs.SetString("unityplugin.keychain", value);
            PlayerPrefs.Save();
        }
    }
}