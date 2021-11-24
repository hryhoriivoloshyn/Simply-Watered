using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simply_Watered.Models
{
    public partial class DeviceTypes
    {

        public DeviceTypes()
        {
            Devices = new HashSet<Devices>();
        }
        public long TypeId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceDescription { get; set; }

        public virtual ICollection<Devices> Devices { get; set; }
    }
}
