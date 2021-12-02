using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simply_Watered.Models;

namespace Simply_Watered.ViewModels
{
    public class RegionsViewModel
    {
        public IEnumerable<Regions> Regions { get; set; }
        public RegionGroups RegionGroup { get; set; } 
        public IEnumerable<IrrigationModes> Modes { get; set; }
        //public IEnumerable<DeviceTypes> Types { get; set; }
    }
}
