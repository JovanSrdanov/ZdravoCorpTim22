using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ZdravoCorpAppTim22.Repository.FileHandlers.Serialization
{
    internal class DoctorToIDConverter : JsonConverter<Doctor>
    {
        public override Doctor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DoctorRepository.Instance.GetByID(reader.GetInt32());
        }

        public override void Write(Utf8JsonWriter writer, Doctor value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.ID);
        }
    }
}