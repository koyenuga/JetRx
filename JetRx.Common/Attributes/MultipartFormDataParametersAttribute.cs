using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetRx.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MultipartFormDataParametersAttribute : Attribute
    {
        public MultipartFormDataParametersAttribute(params string[] parameters)
        {
            Parameters = parameters;
        }

        public IEnumerable<string> Parameters { get; private set; }
    }
}
