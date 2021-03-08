using System.IO;
using UnityEngine;

namespace Elysium.Core
{
    [CreateAssetMenu(fileName = "SavableBoolSO", menuName = "Scriptable Objects/Savable/Bool", order = 1)]
    public class SavableBoolSO : BoolValueSO, ISavable
    {
        public ushort Size => sizeof(bool);

        public void Load(BinaryReader reader)
        {
            Value = reader.ReadBoolean();
        }

        public void LoadDefault()
        {
            Value = defaultValue;
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }
}