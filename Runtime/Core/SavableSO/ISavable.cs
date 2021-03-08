using System;
using System.IO;
using UnityEngine.Events;

namespace Elysium.Core
{
    public interface ISavable
    {
        ushort Size { get; }
        void Load(BinaryReader reader);
        void LoadDefault();
        void Save(BinaryWriter writer);
        event UnityAction OnValueChanged;
    }
}