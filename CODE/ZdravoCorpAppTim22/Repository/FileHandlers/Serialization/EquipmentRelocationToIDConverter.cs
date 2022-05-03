using Repository;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.Repository.FileHandlers.Serialization
{
    internal class EquipmentRelocationToIDConverter : JsonConverter<EquipmentRelocation>
    {
        public override EquipmentRelocation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return EquipmentRelocationRepository.Instance.GetByID(reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, EquipmentRelocation value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Id);
        }
    }
}
