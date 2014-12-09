using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FormatterAPI.Handlers
{
    public class Formatter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }
        
        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Console.WriteLine("Write JSON");
            // Details not important. This code is called and works perfectly.
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Console.WriteLine("Read JSON");

            return null;
            // Details not important. This code is *never* called for some reason.
        }
    }
}