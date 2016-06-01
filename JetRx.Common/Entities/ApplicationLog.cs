using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetRx.Entities
{
    public class ApplicationLog
    {
        public int Id { get; set; }
        public string RawRequest { get; set; }
        public DateTime RecievedTime { get; set; }

    }
}
