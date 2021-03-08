using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Elysium.Utils;

namespace Elysium.Core
{
    public class GenericValueSO<T> : ValueSO
    {
        [SerializeField] protected T value;
        [SerializeField] protected T defaultValue;

        public virtual T Value
        {
            get
            {
                return value;
            }

            set
            {
                //Debug.Log($"{name} Changing Value to {value}.");
                this.value = value;
                InvokeOnValueChanged();
            }
        }

        public override string ValueAsString => Value.ToString();

        protected virtual void SilentlyResetValues() => value = defaultValue;

        private void OnValidate()
        {
#if UNITY_EDITOR
            EditorPlayStateNotifier.RegisterOnExitPlayMode(this, () =>
            {
                // Debug.Log($"OnPlayModeExit: resetting {name}");
                SilentlyResetValues();
            });
#endif
        }
    }
}