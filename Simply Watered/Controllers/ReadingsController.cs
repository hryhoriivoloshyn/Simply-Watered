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
using Simply_Watered.ViewModels;

namespace Simply_Watered.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ReadingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ReadingsController> _logger;

        public ReadingsController(ILogger<ReadingsController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;

        }

        public class DeviceIdModel
        {
            public int id { get; set; }
        }
        [HttpPost("load")]
        public async Task<JsonResult> Load([FromBody] DeviceIdModel deviceIdModel)
        {
           
            if (deviceIdModel != null)
            {
                var deviceId = deviceIdModel.id;
                IEnumerable<DeviceReadings> deviceReadings =  _context.DeviceReadings.Where(r => r.DeviceId == deviceId).ToArray();

                Devices device = _context.Devices.FirstOrDefault(d => d.DeviceId == deviceId);
                device.DeviceType = _context.DeviceTypes.FirstOrDefault(t => t.TypeId == device.TypeId);
                ReadingsViewModel viewModel = new ReadingsViewModel()
                {

                    Readings = deviceReadings,
                    Device = device
                };
                JsonResult response = Json(viewModel);
                return response;
            }

            return null;
        }

    }
}
