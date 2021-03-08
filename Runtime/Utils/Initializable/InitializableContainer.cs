using Elysium.Utils.Attributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elysium.Utils
{
    public class InitializableContainer : MonoBehaviour
    {
        [SerializeField, RequireInterface(typeof(IInitializable))] private List<Object> initializables;
        public List<IInitializable> Initializables => initializables.Cast<IInitializable>().ToList();

        void Start()
        {
            DontDestroyOnLoad(this);
#if UNITY_EDITOR

            foreach (var i in Initializables)
            {
                i.Init();
            }
        }

        void OnDestroy()
        {
            foreach (var i in Initializables)
            {
                i.End();
            }
#endif
        }
    }
}