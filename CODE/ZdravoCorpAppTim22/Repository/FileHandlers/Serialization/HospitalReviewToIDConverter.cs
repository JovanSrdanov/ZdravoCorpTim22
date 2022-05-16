using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model;

namespace Model
{
    public class HospitalReviewToIDConverter: JsonConverter<HospitalReview>
    {
        public override HospitalReview Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return HospitalReviewRepository.Instance.GetByID(reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, HospitalReview value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Id);
        }
    }
}