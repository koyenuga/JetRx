using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetRx.Entities
{
    public class CustomerDevice
    {
        public  int Id { get; set; }
        public int DeviceId { get; set; }
        public int CustomerId { get; set;}

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }
    }
}
