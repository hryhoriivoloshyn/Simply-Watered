using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Simply_Watered.Models
{
    public partial class RegionGroups
    {
        public RegionGroups()
        {
            Regions = new HashSet<Regions>();
        }

        public long RegionGroupId { get; set; }
        public string GroupName { get; set; }
        public string RegionGroupDescription { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Regions> Regions { get; set; }
        public virtual ICollection<IrrigationSchedules> IrrigationSchedules { get; set; }
    }
}
