using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace KozmikAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UploadController : ApiController
    {
        public HttpResponseMessage Post(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/Files/ProfilePics/" + id + ".jpeg");
                    postedFile?.SaveAs(filePath);

                    docfiles.Add(filePath);
                }
                Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                var result = Request.CreateResponse(HttpStatusCode.BadRequest);
                return result;
            }
            return GetFile(id);
        }

        public HttpResponseMessage Get(string id)
        {
            return GetFile(id);
        }

        private static HttpResponseMessage GetFile(string id)
        {
            var filePath = HttpContext.Current.Server.MapPath("~/Files/ProfilePics/");
            var b = File.Exists(filePath + id + ".jpeg") ? File.ReadAllBytes(filePath + id + ".jpeg") : File.ReadAllBytes(filePath + "anonymous.jpg");
            var response =
                new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(Convert.ToBase64String(b))};
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return response;

        }
    }
}
