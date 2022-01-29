using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace DownloadApi.Controllers
{
    public class DownloadController : ApiController
    {
        [HttpGet]
        public async Task<HttpResponseMessage> DownloadFile()
        {

            //test file name
            string fileName = "sample-4.jpg";

            // application hosted physical path (optional)
            string filePath = HostingEnvironment.ApplicationPhysicalPath + @"\temp\" + fileName;

            // reading the file binary data from path (optional if u have alreay a binary)
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);

            // creating the response content body
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            // preparing the stream from the bytes data
            var stream = new MemoryStream(bytes);

            //assigning the stream content 
            result.Content = new StreamContent(stream);

            // getting the mime type of the file by filename to send it in response
            string mimeType = MimeMapping.GetMimeMapping(fileName);

            // setting the file response content type header
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

            // setting the response header as attachment so that it will automatically on client side with the given file name
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
                //string
            };

            return result;
        }

    }
}
