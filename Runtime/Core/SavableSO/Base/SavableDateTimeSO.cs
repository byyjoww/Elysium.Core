using System;
using System.IO;
using UnityEngine;

namespace Elysium.Core
{
    [CreateAssetMenu(fileName = "SavableDateTimeSO", menuName = "Scriptable Objects/Savable/DateTime", order = 1)]
    public class SavableDateTimeSO : DateTimeValueSO, ISavable
    {
        public ushort Size => sizeof(long);

        public void Load(BinaryReader reader)
        {
            Value = DateTime.FromBinary(reader.ReadInt64());
        }

        public void LoadDefault()
        {
            Value = defaultValue;
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(Value.ToBinary());
        }
    }
}