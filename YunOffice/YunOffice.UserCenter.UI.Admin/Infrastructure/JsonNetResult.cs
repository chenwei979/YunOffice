using Newtonsoft.Json;
using System.Web.Mvc;

namespace YunOffice.UserCenter.UI.Admin.Infrastructure
{
    public class JsonNetResult : JsonResult
    {
        public new object Data { get; set; }

        public JsonNetResult()
        {
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";
            if (ContentEncoding != null) response.ContentEncoding = ContentEncoding;

            if (Data != null)
            {
                var writer = new JsonTextWriter(response.Output) { Formatting = Formatting.Indented };
                JsonNet.Serializer.Serialize(writer, Data);
                writer.Flush();
            }
        }
    }


}