using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JetRx.Entities;

namespace JetRx.Api.Controllers
{
    public class BaseController : ApiController
    {
        public JetRxSession userSession
        {
            get
            {
                return (JetRxSession)Request.Properties["userSession"];
            }
        }
    }
}
