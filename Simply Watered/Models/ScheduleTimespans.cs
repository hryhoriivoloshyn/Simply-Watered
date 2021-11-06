using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Simply_Watered.Models
{
    public partial class ScheduleTimespans
    {
        public long TimespanId { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan Finish { get; set; }
        public long IrrigScheduleId { get; set; }

        public virtual IrrigationSchedules IrrigSchedule { get; set; }
    }
}
