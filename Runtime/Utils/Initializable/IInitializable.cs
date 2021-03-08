using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elysium.Utils
{
    public interface IInitializable
    {
        void Init();
        void End();
        bool Initialized { get; }
    }
}