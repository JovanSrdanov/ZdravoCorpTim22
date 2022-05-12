using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.Repository.FileHandlers.Serialization
{
    public class MedicineDataToIDConverter : JsonConverter<MedicineData>
    {
        public override MedicineData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return MedicineDataRepository.Instance.GetByID(reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, MedicineData value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Id);
        }
    }
}
