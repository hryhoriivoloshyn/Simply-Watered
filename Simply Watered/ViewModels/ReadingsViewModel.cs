using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simply_Watered.Models;

namespace Simply_Watered.ViewModels
{
    public class ReadingsViewModel
    {
        public IEnumerable<DeviceReadings> Readings { get; set; }
        public Devices Device { get; set; }
    }
}
