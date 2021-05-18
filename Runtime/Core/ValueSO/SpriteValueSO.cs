using UnityEngine;

namespace Elysium.Core
{
    [CreateAssetMenu(fileName = "SpriteValueSO", menuName = "Scriptable Objects/Value/Sprite", order = 1)]
    public class SpriteValueSO : GenericValueSO<Sprite>
    {
        public override Sprite Value
        {
            get
            {
                if (value == null && defaultValue == null) { throw new System.Exception($"both value and default value for {name} are null"); }

                if (value == null)
                {
                    // Debug.LogError($"value for {name} is null, returning default value"); 
                    return defaultValue;
                }

                return value;
            }

            set
            {
                //Debug.Log($"{name} Changing Value to {value}.");
                this.value = value;
                InvokeOnValueChanged();
            }
        }
    }
}
