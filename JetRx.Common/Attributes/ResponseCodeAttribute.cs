using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JetRx.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ResponseCodesAttribute : Attribute
    {
        public ResponseCodesAttribute(params HttpStatusCode[] statusCodes)
        {
            ResponseCodes = statusCodes;
        }

        public IEnumerable<HttpStatusCode> ResponseCodes { get; private set; }
    }
}
