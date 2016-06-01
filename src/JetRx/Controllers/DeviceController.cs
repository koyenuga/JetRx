using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using JetRx.Entities;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace JetRx.Controllers
{
    [Route("api/[controller]")]
    public class DeviceController : Controller
    {
        
        [HttpPost]
        public Device Register(Device device)
        {
            return new Device();
        }

      
      
    }
}
