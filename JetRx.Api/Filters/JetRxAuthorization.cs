using JetRx.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace JetRx.Api.Filters
{
    public class JetRxAuthorization : AuthorizationFilterAttribute
    {
        public bool MustHaveDeviceKey { get; set; }
        public bool MustHaveAccessKey { get; set; }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var appKey = HttpContext.Current.Request.Headers["x-jetrx-app-key"];
            var deviceKey = HttpContext.Current.Request.Headers["x-jetrx-device-key"];
            var accessKey = HttpContext.Current.Request.Headers["x-jetrx-access-key"];

            if (appKey == null || (MustHaveAccessKey && deviceKey == null)|| (MustHaveAccessKey && accessKey == null))
            {
                var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Oops!!!" };
                throw new HttpResponseException(msg);
            }



            actionContext.Request.Properties["userSession"] = AuthorizationService.AuthenticateRequest(appKey, deviceKey, accessKey, MustHaveAccessKey, MustHaveDeviceKey);
        }
    }
  
}