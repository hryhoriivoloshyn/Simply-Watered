using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simply_Watered.Models;

namespace Simply_Watered.ViewModels
{
    public class DevicesViewModel
    {
        public Regions Region { get; set; }
        public IEnumerable<Devices> Devices { get; set; }
        
        
    }
}
