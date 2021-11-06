using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simply_Watered.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual IEnumerable<RegionGroups> RegionGroups { get; set; }
    }
}
