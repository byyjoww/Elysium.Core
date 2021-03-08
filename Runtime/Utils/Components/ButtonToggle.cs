using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elysium.Utils.Components
{
    public class ButtonToggle : MonoBehaviour
    {
        private bool isActive => gameObject.activeSelf;

        public void Toggle()
        {
            gameObject.SetActive(!isActive);
        }
    }
}