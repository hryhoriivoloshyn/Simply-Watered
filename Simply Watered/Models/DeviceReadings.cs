using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Simply_Watered.Models
{
    public partial class DeviceReadings
    {
        public long ReadingId { get; set; }
        public DateTime ReadingDateTime { get; set; }
        public string NormalizedDate { get; set; }
        public string NormalizedTime { get; set; }
        public double? ReadingTemp { get; set; }
        public double? ReadingHumidity { get; set; }
        public long DeviceId { get; set; }

        public virtual Devices Device { get; set; }
    }
}
