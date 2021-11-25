using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simply_Watered.Models
{
    public class DevicesSchedules
    {
        public long DeviceId { get; set; }
        public long ScheduleId { get; set; }

        public virtual Devices Device { get; set; }
        public virtual IrrigationSchedules Schedule { get; set; }
    }
}
