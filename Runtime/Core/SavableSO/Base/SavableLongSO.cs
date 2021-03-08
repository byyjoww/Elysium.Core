using System.IO;
using UnityEngine;

namespace Elysium.Core
{
    [CreateAssetMenu(fileName = "SavableLongSO", menuName = "Scriptable Objects/Savable/Long", order = 1)]
    public class SavableLongSO : LongValueSO, ISavable
    {
        public ushort Size => sizeof(long);

        public void Load(BinaryReader reader)
        {
            Value = reader.ReadInt64();
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