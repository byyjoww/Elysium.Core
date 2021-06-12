using Elysium.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elysium.Utils.Timers
{
    [Persistent]
    public class Timer : Singleton<Timer>
    {
        [SerializeField, ReadOnly] private List<TimerInstance> activeTimers;

        public static TimerInstance CreateTimer(float _seconds, Func<bool> _destroyCondition, bool _isPersistent = false)
        {
            TimerInstance timer = new TimerInstance(_seconds);
            return Instance.SetupTimer(_destroyCondition, _isPersistent, timer);
        }

        public static TimerInstance CreateEmptyTimer(Func<bool> _destroyCondition, bool _isPersistent = true)
        {
            TimerInstance timer = new TimerInstance();
            return Instance.SetupTimer(_destroyCondition, _isPersistent, timer);
        }

        private TimerInstance SetupTimer(Func<bool> _destroyCondition, bool _isPersistent, TimerInstance timer)
        {
            if (activeTimers == null) { activeTimers = new List<TimerInstance>(); }
            timer.IsPersistent = _isPersistent;
            timer.DestroyCondition = _destroyCondition;
            if (!_isPersistent) { timer.OnEnd += () => activeTimers.Remove(timer); }
            activeTimers.Add(timer);
            return timer;
        }

        public void Update()
        {
            if (activeTimers == null) { return; }

            for (int i = 0; i < activeTimers.Count; i++)
            {
                var timer = activeTimers[i];
                if (timer != null && timer.DestroyCondition?.Invoke() == true) { activeTimers.Remove(timer); }
                if (!timer.IsEnded) { timer.Tick(); }                
            }

            if (activeTimers.Count < 1) { activeTimers = null; }
        }
    }

    [System.Serializable]
    public class TimerInstance
    {
        [field: SerializeField] public float Time { get; set; } = 0;
        public bool IsEnded { get; set; } = false;
        public bool IsPersistent { get; set; } = false;
        public Func<bool> DestroyCondition { get; set; } = default;        

        public event Action OnEnd;
        public event Action OnEndSilent;
        public event Action OnTick;

        public TimerInstance()
        {
            this.Time = 0;
            IsEnded = true;
        }

        public TimerInstance(float _time)
        {
            this.Time = _time;
        }

        public void AddTime(float _time)
        {
            if (_time < 0) { Debug.Log($"trying to set negative time ({_time}) for timer."); }

            this.Time += _time;
            IsEnded = false;
        }

        public void SetTime(float _time)
        {
            if (_time < 0) { Debug.Log($"trying to set negative time ({_time}) for timer."); }

            this.Time = _time;
            IsEnded = false;
        }

        public virtual void Tick()
        {
            if (IsEnded) { return; }
            if (DestroyCondition()) { return; }

            Time -= UnityEngine.Time.deltaTime;
            OnTick?.Invoke();

            if (Time <= 0) { End(); }
        }

        public void End()
        {
            Time = 0;
            IsEnded = true;
            OnEnd?.Invoke();
        }

        public void EndSilent()
        {
            this.Time = 0;
            IsEnded = true;
            OnEndSilent?.Invoke();
        }

        public void Dispose()
        {
            DestroyCondition = () => true;
        }

        public void ClearOnTick()
        {
            OnTick = null;
        }

        public void ClearOnEnd()
        {
            OnEnd = null;
        }

        public void ClearOnEndSilent()
        {
            OnEndSilent = null;
        }             
    }
}
