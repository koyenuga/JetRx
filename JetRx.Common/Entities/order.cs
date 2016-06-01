using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetRx.Entities
{
    public class order
    {
        public int id { get; set; }

        /// <summary>
        /// Customer's prescription details
        /// </summary>
       public CustomerPrescription customer_prescription { get; set; }

        /// <summary>
        /// Order Status
        /// </summary>
        public orderstatus status { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

    }

    public class orderstatus
    {
        public int id { get; set; }
        public string status { get; set; }
        public string description { get; set; }

        public int stepOrder { get; set; }
    }
    
}
