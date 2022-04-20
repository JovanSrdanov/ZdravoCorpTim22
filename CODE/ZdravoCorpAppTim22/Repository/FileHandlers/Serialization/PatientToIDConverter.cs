using Model;
using Repository;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZdravoCorpAppTim22.Repository.FileHandlers.Serialization
{
    internal class PatientToIDConverter : JsonConverter<Patient>
    {
        public override Patient Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return PatientRepository.Instance.GetByID(reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, Patient value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Id);
        }
    }
}
