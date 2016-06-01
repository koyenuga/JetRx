using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetRx.Common.Attributes
{

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class JetRxRequiredAttribute : Attribute
    {
       
        public JetRxRequiredAttribute(params string[] requiredfields)
        {
            RequiredFields = requiredfields;
        }

        public IEnumerable<string> RequiredFields { get; private set; }
    }
}
