using System;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.IO;

namespace itextSharpPDFController
{
    public class BinaryContentResult : ActionResult
    {
        private readonly string contentType;
        private readonly byte[] contentBytes;

        public BinaryContentResult(byte[] contentBytes, string contentType)
        {
            this.contentBytes = contentBytes;
            this.contentType = contentType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.Clear();
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.ContentType = this.contentType;

            using (var stream = new MemoryStream(this.contentBytes))
            {
                stream.WriteTo(response.OutputStream);
                stream.Flush();
            }
        }
    }
}