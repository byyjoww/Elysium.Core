using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elysium.Utils.Components
{
    public class DestroyObject : MonoBehaviour
    {
        public void DestroyThis()
        {
            Destroy(gameObject);
        }

        public void DestroyAnotherObject(GameObject obj)
        {
            Destroy(obj);
        }
    }
}