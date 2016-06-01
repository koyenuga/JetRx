using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using JetRx.Entities;
using JetRx.Api.Providers;
using JetRx.Common.Services;
using System.Threading.Tasks;
using JetRx.Api.Filters;
using JetRx.Common.Attributes;

namespace JetRx.Api.Controllers
{
    [JetRxAuthorization(MustHaveAccessKey =true, MustHaveDeviceKey = true)]
    public class CustomerController : BaseController
    {
        
        // GET: api/Customer/5
        /// <summary>
        /// Get Complete customer details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotFound, HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError)]
        public response<customer> Get()
        {
            response<customer> response = new response<customer>();

            try
            {

            }
            catch
            {

            }

            return response;
        }

        /// <summary>
        /// Update Customer Details
        /// </summary>
        /// <param name="Customer"></param>
        [HttpPut]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotFound, HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError)]
        // PUT: api/Customer/5
        public void Update(customer Customer)
        {
        }

        /// <summary>
        /// Upload Customer Identification
        /// put all the request details in the form-data
        /// image file content part name = 'image'
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotFound, HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError, 
            HttpStatusCode.UnsupportedMediaType)]
        public async  Task<response<identification>> Identification()
        {

            response<identification> response = new response<identification>();
            try
            {

                if (!Request.Content.IsMimeMultipartContent("form-data"))
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                var multipartStreamProvider = new AzureBlobMultipartProvider(AzureUtilities.GetImageBlobContainer("JetRxAzureDocumentStorage"));
                var results =  await Request.Content.ReadAsMultipartAsync(multipartStreamProvider);

                var imageUrl = results.UploadFile();

                var i = new CustomerService().AddCustomerIdentification(new identification {
                     image_id = 0,
                     
                }, imageUrl, userSession);
              
                response.details = i;
                response.issuccessful = true;
                response.message = "Successful";
            }
            catch (Exception e)
            {
                response.issuccessful = false;
                response.message = e.Message;
            }

            return response;


        }

        /// <summary>
        ///Upload Customer Insurance
        /// put all the request details in the form-data
        /// image file content part name = 'image'
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotFound, HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError,
            HttpStatusCode.UnsupportedMediaType)]
        public async Task<response<insurance>> Insurance()
        {
            //LoggingService.LogRequest(await Request.Content.ReadAsStringAsync());
            response<insurance> response = new response<insurance>();
            try
            {

                if (!Request.Content.IsMimeMultipartContent("form-data"))
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                var multipartStreamProvider = new AzureBlobMultipartProvider(AzureUtilities.GetImageBlobContainer("JetRxAzureDocumentStorage"));
                var results = await Request.Content.ReadAsMultipartAsync(multipartStreamProvider);

                var imageUrl = results.UploadFile();
               

                var i = new CustomerService().AddCustomerInsurance(new insurance
                {
                    image_id = 0,

                }, imageUrl, userSession);

                response.details = i;
                response.issuccessful = true;
                response.message = "Successful";
            }
            catch (Exception e)
            {
                response.issuccessful = false;
                response.message = e.Message;
            }

            return response;


        }


    }
}
