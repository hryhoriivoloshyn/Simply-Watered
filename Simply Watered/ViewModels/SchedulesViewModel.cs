using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simply_Watered.Models;

namespace Simply_Watered.ViewModels
{
    public class SchedulesViewModel
    {
        public IEnumerable<IrrigationSchedules> Schedules { get; set; }
        public RegionGroups RegionGroup { get; set; }
        public IEnumerable<IrrigationModes> IrrigationModes { get; set; }
        public string MinStartDate { get; set; }
        public string MinEndDate { get; set; }
    }
}
