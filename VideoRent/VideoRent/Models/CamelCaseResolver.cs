using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace VideoRent.Models
{
    public class CamelCaseResolver : ActionResult
    {
        public int TotalRecords { get; set; }
        public int RecordsSearched { get; set; }
        public Encoding ContentEncoding { get; set; }
        public string ContentType { get; set; }
        public object Data { get; set; }
        public JsonRequestBehavior JsonRequestBehavior { get; set; }


        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw  new ArgumentNullException(nameof(context));
            
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Get request blocked for savety. Allow get request by setting JsonRequestBehavior to AllowGet");

            var respone = context.HttpContext.Response;
            respone.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null)
                respone.ContentEncoding = ContentEncoding;

            if (Data != null)
            {
                var camelCaseFormater = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                var serialize = JsonConvert.SerializeObject(Data, Formatting.Indented, camelCaseFormater);
                respone.Write(serialize);
            }
        }
    }
}