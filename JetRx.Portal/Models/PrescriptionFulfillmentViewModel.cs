using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JetRx.Entities;

namespace JetRx.Portal.Models
{
    public class PrescriptionFulfillmentViewModel
    {
       public customer Customer { get; set;}
        public prescription Prescription { get; set; }

        public insurance CustomerInsurance { get; set; }

        public identification CustomerIdentification { get; set; }
    }
}