
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MetaEvent
    {
        private event Action action = delegate { };
        public void Invoke() {
            action?.Invoke();
        }
        public void AddListener(Action listner) { action += listner; }
        public void RemoveListener(Action listner) { action -= listner; }
    }

    public class MetaEvent<T> {
        private event Action<T> action = delegate { };
        public void Invoke(T param)
        {
            action?.Invoke(param);
        }
        public void AddListener(Action<T> listner) { action += listner; }
        public void RemoveListener(Action<T> listner) { action -= listner; }

       
    }

    public class MetaEvent<T1, T2>
    {
        private event Action<T1, T2> action = delegate { };
        public void Invoke(T1 param1, T2 param2)
        {
            action?.Invoke(param1,param2);
        }
        public void AddListener(Action<T1,T2> listner) { action += listner; }
        public void RemoveListener(Action<T1, T2> listner) { action -= listner; }

    }
    public class MetaEvent<T1, T2, T3>
    {
        private event Action<T1, T2, T3> action = delegate { };
        public void Invoke(T1 param1, T2 param2, T3 param3)
        {
            action?.Invoke(param1, param2, param3);
        }
        public void AddListener(Action<T1, T2, T3> listner) { action += listner; }
        public void RemoveListener(Action<T1, T2, T3> listner) { action -= listner; }

    }

    public class MetaEvent<T1, T2, T3, T4>
    {
        private event Action<T1, T2, T3, T4> action = delegate { };
        public void Invoke(T1 param1, T2 param2, T3 param3, T4 param4)
        {
            action?.Invoke(param1, param2, param3, param4);
        }
        public void AddListener(Action<T1, T2, T3, T4> listner) { action += listner; }
        public void RemoveListener(Action<T1, T2, T3, T4> listner) { action -= listner; }        
    }
