using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simply_Watered.Models
{
    public class IrrigationHistory
    {
        public long Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public string NormalizedStartDate { get; set; }
        public string NormalizedStartTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string NormalizedEndDate { get; set; }
        public string NormalizedEndTime { get; set; }
        public double? ReadingStartTemp { get; set; }
        public double? ReadingStartHumidity { get; set; }
        public double? ReadingEndTemp { get; set; }
        public double? ReadingEndHumidity { get; set; }
        public long DeviceId { get; set; }
        public long? IrrigModeId { get; set; }
        public virtual Devices Device { get; set; }
        public virtual IrrigationModes IrrigMode { get; set; }
    }
}
