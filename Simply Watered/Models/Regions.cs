using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Simply_Watered.Models
{
    public partial class Regions
    {
        public Regions()
        {
            Devices = new HashSet<Devices>();
        }

        public long RegionId { get; set; }
        public string RegionName { get; set; }
        public string RegionDescription { get; set; }
        public long? RegionGroupId { get; set; }

        public virtual RegionGroups RegionGroup { get; set; }
        public virtual ICollection<Devices> Devices { get; set; }
    }
}
