using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetRx.Entities
{
    public class Customer : User
    {
        public List<Device> Devices { get; set; }
    }
}
