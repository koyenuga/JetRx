using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace JetRx.Entities
{
    public class response<T>
    {
        public bool issuccessful { get; set; }
        public string message { get; set;}
        public string httpstatuscode { get; set;}
        public T details { get; set;}
    }
}
