using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetRx.Entities;
using JetRx.Common.Data.SqlServer;

namespace JetRx.Common
{
    public class DeviceManager
    {
        public device RegisterDevice(device Device)
        {

            return new DeviceDataAccess().RegisterDevice(Device);
           
        }
    }
}
