using System;
using UnityEngine;

namespace Elysium.Core
{
    public class GenericEventSO<T1> : ScriptableObject
    {

#pragma warning disable 0067
        public event Action<T1> OnRaise;
#pragma warning restore 0067

        public void Raise(T1 data)
        {
            OnRaise?.Invoke(data);
        }

        public event Action OnRequestList;

        public void RequestRaise()
        {
            OnRequestList?.Invoke();
        }
    }

    public class GenericEventSO<T1, T2> : ScriptableObject
    {

#pragma warning disable 0067
        public event Action<T1, T2> OnRaise;
#pragma warning restore 0067

        public void Raise(T1 data, T2 data2)
        {
            OnRaise?.Invoke(data, data2);
        }

        public event Action OnRequestList;

        public void RequestRaise()
        {
            OnRequestList?.Invoke();
        }
    }

    public class GenericEventSO<T1, T2, T3> : ScriptableObject
    {

#pragma warning disable 0067
        public event Action<T1, T2, T3> OnRaise;
#pragma warning restore 0067

        public void Raise(T1 data, T2 data2, T3 data3)
        {
            OnRaise?.Invoke(data, data2, data3);
        }

        public event Action OnRequestList;

        public void RequestRaise()
        {
            OnRequestList?.Invoke();
        }
    }

    public class GenericEventSO<T1, T2, T3, T4> : ScriptableObject
    {

#pragma warning disable 0067
        public event Action<T1, T2, T3, T4> OnRaise;
#pragma warning restore 0067

        public void Raise(T1 data, T2 data2, T3 data3, T4 data4)
        {
            OnRaise?.Invoke(data, data2, data3, data4);
        }

        public event Action OnRequestList;

        public void RequestRaise()
        {
            OnRequestList?.Invoke();
        }
    }
}