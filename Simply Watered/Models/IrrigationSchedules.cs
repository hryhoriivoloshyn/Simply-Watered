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
            //DevicesSchedules = new HashSet<DevicesSchedules>();
        }

        public long IrrigScheduleId { get; set; }
        public string IrrigScheduleName { get; set; }
        public DateTime ScheduleStartDate { get; set; }
        public DateTime ScheduleEndDate { get; set; }
        public long RegionGroupId { get; set; }


        //public virtual Devices Device { get; set; }
        public virtual ICollection<ScheduleTimespans> ScheduleTimespans { get; set; }
        //public virtual ICollection<DevicesSchedules> DevicesSchedules { get; set; }
        public virtual RegionGroups RegionGroup { get; set; }
    }
}
