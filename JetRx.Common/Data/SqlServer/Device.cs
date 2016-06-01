namespace JetRx.Common.Data.SqlServer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Device")]
    public partial class Device
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string AppDeviceKey { get; set; }

        [StringLength(50)]
        public string DeviceType { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string DeviceIdentifier { get; set; }

        [StringLength(50)]
        public string DeviceName { get; set; }
    }
}
