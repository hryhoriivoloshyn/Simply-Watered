using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Simply_Watered.Models
{
    public partial class Devices
    {
        public Devices()
        {
            DeviceReadings = new HashSet<DeviceReadings>();
            DevicesSchedules = new HashSet<DevicesSchedules>();
        }

        public long DeviceId { get; set; }
        public long? IrrigModeId { get; set; }
        public long? RegionId { get; set; }
        public long? IrrigScheduleId { get; set; }
        public double? MinimalHumidity { get; set; }
        public double? MaxHumidity { get; set; }
        public string SerialNumber { get; set; }
        public long? TypeId { get; set; }

        public virtual IrrigationModes IrrigMode { get; set; }
        public virtual Regions Region { get; set; }

        public virtual DeviceTypes DeviceType { get; set; }
        public virtual ICollection<DeviceReadings> DeviceReadings { get; set; }
        public virtual  ICollection<IrrigationHistory> IrrigationHistories { get; set; }
        //public virtual ICollection<IrrigationSchedules> IrrigationSchedules { get; set; }

        public virtual ICollection<DevicesSchedules> DevicesSchedules { get; set; }
    }
}
