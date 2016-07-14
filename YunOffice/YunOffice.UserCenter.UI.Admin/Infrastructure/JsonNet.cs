using Newtonsoft.Json;
using System.IO;

namespace YunOffice.UserCenter.UI.Admin.Infrastructure
{
    public static class JsonNet
    {
        public static JsonSerializer Serializer { get; set; }

        static JsonNet()
        {
            Serializer = JsonSerializer.Create(new JsonSerializerSettings());
        }

        public static string Encode(object value)
        {
            var stringWriter = new StringWriter();
            var writer = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
            Serializer.Serialize(writer, value);
            writer.Flush();

            return stringWriter.ToString();
        }
    }
}