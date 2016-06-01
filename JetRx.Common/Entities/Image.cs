using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetRx.Entities
{
    public class image
    {
        public int id { get; set;}

        /// <summary>
        /// type of image
        /// </summary>
        public imagetype image_type { get; set; }

        public int owner_id { get; set; }

        public string url { get; set; }
    }

    public enum imagetype
    {
        Prescription = 0,
        PrescriptionRefill =1,
        InsuranceCard = 2,
        DriversLicense =3,
        CreditCard = 4,
        Identification = 5,

    }
}
