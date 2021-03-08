using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elysium.Utils
{
    [System.Serializable]
    public class RefValue<T>
    {
        [SerializeField] protected T value = default;
        protected Func<T> getValue;

        public event Action<T, T> OnChanged;

        public T Value
        {
            get
            {
                Recalculate();
                return value;
            }
        }

        public RefValue(Func<T> lambda)
        {
            getValue = lambda;
            SetValue();
        }

        public void Recalculate()
        {
            T prev = value;
            SetValue();
            if (!EqualityComparer<T>.Default.Equals(prev, value)) { OnChanged?.Invoke(prev, value); }
        }

        protected T SetValue()
        {
            if (getValue != null) { value = getValue(); }
            return value;
        }        
    }
}