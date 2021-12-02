using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simply_Watered.Data;
using Simply_Watered.Models;

namespace Simply_Watered.Services
{
    public static class DeviceManager
    {
         public static IEnumerable<Devices> GetDevicesByGroupId(long groupId, ApplicationDbContext _context)
        {
            long[] regionIds = _context.Regions
                .Where(r => r.RegionGroupId == groupId)
                .Select(r => r.RegionId)
                .ToArray();

            IEnumerable<Devices> devices = _context.Devices
                .Where(d => regionIds.Contains((long)d.RegionId))
                .ToList();

            return devices;
        }
    }
}
