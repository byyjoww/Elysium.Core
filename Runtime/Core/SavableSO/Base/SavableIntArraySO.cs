using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Elysium.Core
{
    [CreateAssetMenu(fileName = "SavableIntArraySO", menuName = "Scriptable Objects/Savable/Int Array", order = 1)]
    public class SavableIntArraySO : IntArrayValueSO, ISavable
    {
        public ushort Size
        {
            get
            {
                Stream stream = new MemoryStream();
                IFormatter formatter = new BinaryFormatter();

                formatter.Serialize(stream, Value);
                ushort length = (ushort)stream.Length;
                stream.Dispose();

                return length;
            }
        }

        public void Load(BinaryReader reader)
        {
            var length = reader.ReadInt32();
            Value = new int[length];

            for (int i = 0; i < length; i++)
            {
                Value[i] = reader.ReadInt32();
            }
        }

        public void LoadDefault()
        {
            Value = defaultValue;
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(Value.Length);

            for (int i = 0; i < Value.Length; i++)
            {
                writer.Write(Value[i]);
            }
        }
    }
}