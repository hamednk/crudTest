namespace Mc2.CrudTest.Application.DTO.JsonConverter
{
    using Newtonsoft.Json;
    using System;

    public class CommaNumberConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(CommaAppendToNumber(float.Parse(value.ToString())));
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return RemoveCommaFromNumber(reader.Value.ToString());
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        string CommaAppendToNumber(float? number)
        {
            if (!number.HasValue)
                return null;

            return number.Value.ToString("#,##0");
        }

        float RemoveCommaFromNumber(string number)
        {
            return float.Parse(number.Replace(",", ""));
        }
    }
}
