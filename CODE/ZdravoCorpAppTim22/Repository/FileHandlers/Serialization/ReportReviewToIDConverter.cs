using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;

namespace Model
{
    public class ReportReviewToIDConverter : JsonConverter<ReportReview>
    {
        public override ReportReview Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return ReportReviewRepository.Instance.GetByID(reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, ReportReview value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Id);
        }
    }
}