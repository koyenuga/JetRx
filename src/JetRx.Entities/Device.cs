using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetRx.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public string DeviceKey { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string PhoneNumber { get; set; }
    }
}
