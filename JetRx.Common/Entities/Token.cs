using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetRx.Entities
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Secret { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        //public ApplicationTypes ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        [MaxLength(100)]
        public string AllowedOrigin { get; set; }
    }

    public class AccessToken
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClientId { get; set; }
        public int DeviceId { get; set; }

        [MaxLength(100)]
        public string Secret { get; set; }
        public DateTime? IssuedUtc { get; set; }
        public DateTime? ExpiresUtc { get; set; }
        
    }
}
