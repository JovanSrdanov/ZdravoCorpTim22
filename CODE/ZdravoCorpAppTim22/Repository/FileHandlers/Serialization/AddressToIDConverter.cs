using Repository;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZdravoCorpAppTim22.Repository.FileHandlers.Serialization
{
    internal class AddressToIDConverter : JsonConverter<Address>
    {
        public override Address Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return AddressRepository.Instance.GetByID(reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, Address value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Id);
        }
    }
}
