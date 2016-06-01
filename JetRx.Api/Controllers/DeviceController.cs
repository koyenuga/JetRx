using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JetRx.Common.Services;
using JetRx.Common.Extensions;
using JetRx.Entities;
using JetRx.Api.Filters;
using JetRx.Common.Attributes;

namespace JetRx.Api.Controllers
{
    [JetRxAuthorization]
    public class DeviceController : BaseController
    {



        /// <summary>
        /// This API Method is responsible for Registering a device with JetRx, A device can only be registered once.
        /// A appDeviceKey will be returned (This key needs to be stored on the device, will be needed to make subsequent API Calls)
        /// </summary>
        /// <param name="Device">Device Information</param>
        /// <returns>Registered Device</returns>
        ///<exception></exception>
        [HttpPost]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotFound, HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError)]
        [JetRxRequired("phonenumber")]
        public response<device> Register(device Device)
        {
            if (string.IsNullOrWhiteSpace(Device.phonenumber))
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Invalid Phone Number" });
          

            response<device> response = new response<device>(); 
            try
            {

                response.details = new AuthorizationService().RegisterDevice(userSession.AppId, Device);
                response.issuccessful = true;
                response.message = "Successful";
            }
            catch(Exception e)
            {
                response.issuccessful = false;
                response.message = e.Message;
            }

            return response;
        }
    }
}
