using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetRx.Entities
{
    public class ApplicationConfig
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
