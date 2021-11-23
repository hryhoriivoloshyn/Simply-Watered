using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simply_Watered.Models;

namespace Simply_Watered.ViewModels
{
    public class DevicesViewModel
    {
        public IEnumerable<Devices> Devices { get; set; }
        public Regions Region { get; set; }
    }
}
