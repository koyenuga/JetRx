using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetRx.Entities
{
    public class prescription
    {
        public int id { get; set; }
        public string doctor_name { get; set; }
        public string doctor_address { get; set; }
        public string doctor_phonenumber { get; set; }
        public int image_id { get; set;}
        public PrescriptionProduct prescription_product { get; set; }

        public int quantity { get; set; }
        public DateTime? prescribed_date { get; set; }

        public int duration { get; set; }
        public bool refill { get; set; }

        public string barcode { get; set; }

        public string prescription_type { get; set; }

        /// <summary>
        /// Required if prescription_type is 'transfer'
        /// </summary>
        public string transfer_pharmacy_name { get; set; }

        /// <summary>
        /// Required if prescription_type is 'transfer'
        /// </summary>
        public string transfer_pharmacy_phone_number { get; set; }

        /// <summary>
        /// Required if prescription_type is 'transfer'
        /// </summary>
        public string transfer_prescription_number { get; set; }

    }

    public class PrescriptionProduct
    {
        public int id { get; set;}
        public string product_details { get; set; }
        
        public string[] video_urls { get; set; }
        public string image_url { get; set; }
        public string unit { get; set; }
        public string cost_per_unit { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }
    }

    public class CustomerPrescription
    {
        public int Id { get; set; }
        public customer Customer { get; set; }
        public prescription Prescription { get; set; }
        public device Device { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }



    }

    
}


