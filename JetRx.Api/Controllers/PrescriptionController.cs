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
using RestSharp;
using JetRx.Api.Filters;
using JetRx.Common.Attributes;

namespace JetRx.Api.Controllers
{
    [JetRxAuthorization(MustHaveAccessKey = true, MustHaveDeviceKey = true)]
    public class PrescriptionController : BaseController
    {
        public string appKey;

        // GET: api/Prescription
        /// <summary>
        /// Returns all customer prescriptions
        /// </summary>
        /// <returns></returns>
        /// 
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotFound, HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError,
          HttpStatusCode.UnsupportedMediaType, HttpStatusCode.Unauthorized)]
        public response<List<prescription>> Get()
        {
            response<List<prescription>> response = new response<List<prescription>>();
            try
            {
                response.details = new CustomerService().GetPrescriptions(userSession.Customer.id);
                response.issuccessful = true;
                return response;
            }
            catch(Exception e)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        // GET: api/Prescription/5
        /// <summary>
        /// return specific customer prescription [id = prescritionId]
        /// </summary>
        /// <param name="id"> prescription Id</param>
        /// <returns></returns>
        /// 
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotFound, HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError,
       HttpStatusCode.UnsupportedMediaType, HttpStatusCode.Unauthorized)]
        public response<prescription> Get(int id)
        {
            response<prescription> response = new response<prescription>();
            try
            {
                response.details = new CustomerService().GetPrescription(id);
                response.issuccessful = true;
                return response;
            }
            catch (Exception e)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        // POST: api/Prescription
        /// <summary>
        /// Create new prescription using multipart mime.
        /// put all the request details in the form-data
        /// image file content part name = 'image'
        /// id, if Id is not present, it wil be treated as a new prescription
        /// type = prescription type, new, refil or transfer
        ///doctor_name
        ///doctor_address
        ///doctor_phonenumber
        ///prescription_product_details"
        /// </summary>
        /// 
        [HttpPost]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotFound, HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError,
          HttpStatusCode.UnsupportedMediaType, HttpStatusCode.Unauthorized)]
        public  async Task<response<prescription>> Post()
        {
           // LoggingService.LogRequest(await Request.Content.ReadAsStringAsync());
            response<prescription> response = new response<prescription>();
            try
            {
              

                if (!Request.Content.IsMimeMultipartContent("form-data"))
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                var multipartStreamProvider = new AzureBlobMultipartProvider(AzureUtilities.GetImageBlobContainer("JetRxAzureDocumentStorage"));
                var results =  await  Request.Content.ReadAsMultipartAsync(multipartStreamProvider);           
                var imageUrl = results.UploadFile();
                var prescriptionType = !results.FormFields.ContainsKey("type") ? string.Empty : results.FormFields["type"];
                bool isRefil = false;
                if ((!string.IsNullOrEmpty(prescriptionType)) && prescriptionType.ToLower().StartsWith("refill"))
                    isRefil = true;
                
                var p = new CustomerService().SaveOrder(new prescription
                {
                   id = !results.FormFields.ContainsKey("Id") ? 0 : int.Parse(results.FormFields["Id"]),
                    doctor_address = !results.FormFields.ContainsKey("doctor_address") ? string.Empty: results.FormFields["doctor_address"],
                    doctor_name = !results.FormFields.ContainsKey("doctor_name") ? string.Empty : results.FormFields["doctor_name"],
                    doctor_phonenumber = !results.FormFields.ContainsKey("doctor_phonenumber") ? string.Empty : results.FormFields["doctor_phonenumber"],
                    prescription_type = prescriptionType,
                    duration = !results.FormFields.ContainsKey("duration") ? 0 : int.Parse(results.FormFields["duration"]),
                    refill = isRefil,
                     prescription_product = new PrescriptionProduct {
                          product_details = !results.FormFields.ContainsKey("prescription_product_details") ? string.Empty : results.FormFields["prescription_product_details"],
                         

                     },
                    image_id = 0,
                     
                }, imageUrl, userSession);

                response.details = p;
                response.issuccessful = true;
                response.message = "Successful";
            }
            catch (Exception e)
            {
                response.issuccessful = false;
                response.message = e.Message + e.StackTrace;
            }

            return response;
           
           
        }

        //// PUT: api/Prescription/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Prescription/5
        //public void Delete(int id)
        //{
        //}


     
    }
}
