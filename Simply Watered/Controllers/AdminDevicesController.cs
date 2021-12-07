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

        [HttpPut("{deviceId:long}")]
        public async Task<IActionResult> DisableDevice(long deviceId)
        {
            var device = await _context.Devices.FirstAsync(d => d.DeviceId == deviceId);

            return Ok(deviceId);
        }
    }

}
