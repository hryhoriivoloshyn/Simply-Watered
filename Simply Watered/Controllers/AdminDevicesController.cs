using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Simply_Watered.Data;
using Simply_Watered.Models;

namespace Simply_Watered.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDevicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AdminDevicesController> _logger;

        public AdminDevicesController(ILogger<AdminDevicesController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IEnumerable<Devices>> Get()
        {
            IEnumerable<Devices> devices = await _context.Devices.ToListAsync();
            IEnumerable<DeviceTypes> types = await _context.DeviceTypes.ToListAsync();
            return devices;
        }

        [HttpPut("{deviceId:long}")]
        public async Task<IActionResult> ChangeDeviceActive(long deviceId)
        {
            var device = await _context.Devices.FirstAsync(d => d.DeviceId == deviceId);
            if (device == null)
            {
                return BadRequest(deviceId);
            }

            device.Active = device.Active != true;
            
            _context.Update(device);
            await _context.SaveChangesAsync();
            return Ok(deviceId);
        }


    }

}
