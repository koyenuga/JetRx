using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JetRx.Entities
{
    public class customer : user
    {
        /// <summary>
        /// Id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Customer's First Name
        /// </summary>
        [MaxLength(75)]
        public string firstname { get; set; }

        /// <summary>
        /// Customer's Last Name
        /// </summary>
        [MaxLength(75)]
        public string lastname { get; set; }

        /// <summary>
        /// Customer's Identification Details including image.
        /// </summary>
        public identification Identification { get; set; }

        /// <summary>
        /// Customer's Insurance information
        /// </summary>
        public insurance Insurance { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

    }

    public class identification
    {
        public int id { get; set; }
        /// <summary>
        /// Identification type such as (Driver's license or passport)
        /// </summary>
        public string identification_type { get; set; }

        public int image_id { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }
    }

    public class insurance
    {
        public int id { get; set; }
      
        /// <summary>
        /// Insurance provider name
        /// </summary>
        public string provider_name { get; set; }
        /// <summary>
        /// Insurance Provider Phone number
        /// </summary>
        public string provider_phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int image_id { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }
    }
    public class CustomerIdentification
    {
        public int Id { get; set; }
        public int IdentificationId { get; set; }
        public int CustomerId { get; set; }
        public int DeviceId { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

    }

    public class CustomerInsurance
    {
        public int Id { get; set; }
        public int InsuranceId { get; set; }
        public int CustomerId { get; set; }
        public int DeviceId { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

    }
}
