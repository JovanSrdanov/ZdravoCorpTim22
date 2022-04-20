using Model;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZdravoCorpAppTim22.Repository.FileHandlers.Serialization
{
    internal class EquipmentToIDConverter : JsonConverter<Equipment>
    {
        public override Equipment Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return EquipmentRepository.Instance.GetByID(reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, Equipment value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Id);
        }
    }
}
