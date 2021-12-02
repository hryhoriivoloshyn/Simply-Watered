using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Simply_Watered.Models
{
    public partial class IrrigationModes
    {
        public IrrigationModes()
        {
            Devices = new HashSet<Devices>();
        }

        public long IrrigModeId { get; set; }
        public string ModeName { get; set; }

        public virtual ICollection<Devices> Devices { get; set; }
        public virtual ICollection<IrrigationHistory> IrrigationHistories { get; set; }
    }
}
