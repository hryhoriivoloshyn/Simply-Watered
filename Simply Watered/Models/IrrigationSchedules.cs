using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Simply_Watered.Models
{
    public partial class IrrigationSchedules
    {
        public IrrigationSchedules()
        {
            ScheduleTimespans = new HashSet<ScheduleTimespans>();
        }

        public long IrrigScheduleId { get; set; }
        public string IrrigScheduleName { get; set; }
        public DateTime SheduleStartDate { get; set; }
        public DateTime? ScheduleEndDate { get; set; }
        public long DeviceId { get; set; }

        public virtual Devices Device { get; set; }
        public virtual ICollection<ScheduleTimespans> ScheduleTimespans { get; set; }
    }
}
