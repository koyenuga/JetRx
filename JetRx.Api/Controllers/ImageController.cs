using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using JetRx.Entities;
using System.Web;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using JetRx.Api.Providers;
using JetRx.Api.Filters;
using JetRx.Common.Services;
using JetRx.Common.Attributes;

namespace JetRx.Api.Controllers
{
    [JetRxAuthorization(MustHaveAccessKey = true, MustHaveDeviceKey = true)]
    public class ImageController : ApiController
    {
        // GET: api/Image
        /// <summary>
        /// Get All Customer Images
        /// </summary>
        /// <returns></returns>
        public List<image> Get()
        {
            return new List<image>();
        }

        // GET: api/Image/5
        /// <summary>
        /// Get Image by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public image Get(int id)
        {
            return new image();
        }

        
       /// <summary>
       /// 
       /// </summary>
       /// <param name="type"></param>
       /// <returns></returns>
        [HttpPost]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotFound, HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError,
            HttpStatusCode.UnsupportedMediaType)]
        public List<image> Upload()
        {

            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var multipartStreamProvider = new AzureBlobMultipartProvider(AzureUtilities.GetImageBlobContainer("JetRxAzureDocumentStorage"));
            var results = Request.Content.ReadAsMultipartAsync<AzureBlobMultipartProvider>(multipartStreamProvider);

            return results.Result.UploadFiles(imagetype.CreditCard, 0);



        }


        // DELETE: api/Image/5
        /// <summary>
        /// Delete Image
        /// </summary>
        /// <param name="id"></param>
        /// 
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}
