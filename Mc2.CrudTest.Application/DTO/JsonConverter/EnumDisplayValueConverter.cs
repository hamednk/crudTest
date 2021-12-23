using System.Collections.Generic;
using System.Reflection;

namespace Mc2.CrudTest.Application.DTO.JsonConverter
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    public class EnumDisplayValueConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                Enum @enum = (Enum)value;
                writer.WriteValue(GetEnumDisplayValue(@enum));
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Enum.Parse(objectType, GetDisplayValueToEnumValue(objectType, reader.Value?.ToString()), true);
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        string GetEnumDisplayValue(Enum enumName)
        {
            Type type = (Type)enumName.GetType();
            FieldInfo? field = type.GetField(enumName.ToString());
            DisplayAttribute? display = ((DisplayAttribute[])field?.GetCustomAttributes(typeof(DisplayAttribute), false))?.FirstOrDefault();
            return display != null
                ? display.Name
                : enumName.ToString();
        }

        string GetDisplayValueToEnumValue(Type type, string displayName)
        {
            List<FieldInfo> fields = type.GetFields().ToList();
            foreach (FieldInfo fieldInfo in fields)
            {
                DisplayAttribute? display = ((DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false)).FirstOrDefault();
                if (display == null ||
                     displayName != display.GetName())
                    continue;
                return fieldInfo.Name;
            }

            return displayName;
        }
    }
}
