using System.IO;
using UnityEngine;

namespace Elysium.Core
{
    [CreateAssetMenu(fileName = "SavableIntSO", menuName = "Scriptable Objects/Savable/Int", order = 1)]
    public class SavableIntSO : IntValueSO, ISavable
    {
        public ushort Size => sizeof(int);

        public void Load(BinaryReader reader)
        {
            Value = reader.ReadInt32();
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