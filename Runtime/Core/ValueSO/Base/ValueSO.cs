using System;
using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Core
{
    public abstract class ValueSO : ScriptableObject
    {
        public abstract string ValueAsString { get; }

#pragma warning disable 0067
        public event UnityAction OnValueChanged;
#pragma warning restore 0067

        protected void InvokeOnValueChanged()
        {
            OnValueChanged?.Invoke();
        }
    }
}