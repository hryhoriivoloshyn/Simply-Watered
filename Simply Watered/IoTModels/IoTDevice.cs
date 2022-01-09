using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simply_Watered_IOT.Models
{
   public class IoTDevice
    {
        public string SerialNumber { get; set; }
        public long DeviceId { get; set; }
        public long IrrigModeId { get; set; }
        public double MinimalHumidity { get; set; }
        public double MaxHumidity { get; set; }
        public bool? Active { get; set; }
        public IoTSchedule Schedule { get; set; }
    }
}
