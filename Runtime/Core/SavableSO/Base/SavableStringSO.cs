using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Elysium.Core
{
    [CreateAssetMenu(fileName = "SavableStringSO", menuName = "Scriptable Objects/Savable/String", order = 1)]
    public class SavableStringSO : StringValueSO, ISavable
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
            Value = reader.ReadString();
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