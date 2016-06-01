using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetRx.Entities
{
    public class JetRxSession
    {
        public customer Customer { get; set; }
        public device Device { get; set; }
        public int AppId { get; set; }
        public string Accesskey { get; set; }
    }
}
