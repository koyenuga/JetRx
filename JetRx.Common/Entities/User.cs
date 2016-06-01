using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JetRx.Entities
{
    public class user
    {
        /// <summary>
        /// This is an access token assigned to the user upon successful login. it expires after 30 mins of no user interaction with the api.
        /// </summary>
        public string accesstoken { get; set; }
        /// <summary>
        /// User's email address
        /// </summary>
        /// 
        
        public string email { get; set; }
        /// <summary>
        /// User's password
        /// </summary>
        /// 
        [Required]
        public string password { get; set; }
        /// <summary>
        /// User's main phone number
        /// </summary>
        
        [Phone(ErrorMessage ="Invalid Phone Number")]
        public string phone_number { get; set; }
    }
}
