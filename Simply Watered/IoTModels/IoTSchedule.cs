using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simply_Watered_IOT.Models
{
   public class IoTSchedule
    {
        public long IrrigScheduleId { get; set; }
        public string IrrigScheduleName { get; set; }
        public DateTime ScheduleStartDate { get; set; }
        public DateTime ScheduleEndDate { get; set; }

        public TimeSpan Start { get; set; }
        public TimeSpan Finish { get; set; }
    }
}
