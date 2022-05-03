using Model;
using Repository;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZdravoCorpAppTim22.Repository.FileHandlers.Serialization
{
    internal class RoomToIDConverter : JsonConverter<Room>
    {
        public override Room Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return RoomRepository.Instance.GetByID(reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, Room value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Id);
        }
    }
}
