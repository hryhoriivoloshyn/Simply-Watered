using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    public class DevicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<DevicesController> _logger;

        public DevicesController(ILogger<DevicesController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;

        }
        public class RegionIdModel
        {
            public int id { get; set; }
        }
        [HttpPost("load")]
        public async Task<JsonResult> Load([FromBody] RegionIdModel regionIdModel)
        {
           
            if (regionIdModel != null)
            {
                var regionId = regionIdModel.id;
                IEnumerable<Devices> devices = _context.Devices.Where(r => r.RegionId == regionId).ToArray();
                foreach (var device in devices)
                {
                    device.DeviceType = await _context.DeviceTypes.Where(t => t.TypeId == device.TypeId).FirstOrDefaultAsync();
                }
                Regions region = await _context.Regions.Where(g => g.RegionId == regionId).FirstOrDefaultAsync();
                DevicesViewModel viewModel = new DevicesViewModel()
                {
                  
                    Devices = devices,
                    Region=region
                };
                JsonResult response = Json(viewModel);
                return response;
            }

            return null;
        }

        public class DeleteDeviceModel
        {

            public long id { get; set; }
        }
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteDeviceModel deviceModel)
        {

            
            if (deviceModel != null)
            {
                var deviceId = deviceModel.id;
                Devices device = _context.Devices.FirstOrDefault(r => r.DeviceId == deviceId);
                if (device != null)
                {
                    device.RegionId = null;
                    _context.Devices.Update(device);
                    await _context.SaveChangesAsync();
                    return Ok(deviceModel);
                }

            }

            return NotFound();

        }

        public class InputModel
        {
         
            public long regionId { get; set; }
            public string SerialNumber { get; set; }
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] InputModel inputModel)
        {
            if (inputModel != null)
            {
                
                var serialNumber = inputModel.SerialNumber;
                var regionId = inputModel.regionId;
                Devices device = _context.Devices.FirstOrDefault(r => r.SerialNumber == serialNumber);
                
                if (device != null)
                {
                    if (device.RegionId != null)
                    {
                        ResponseError error = new ResponseError("DeviceIsTaken");
                        return NotFound(error);
                    }

                    device.RegionId = regionId;
                    _context.Devices.Update(device);
                    await _context.SaveChangesAsync();
                    return Ok(inputModel);
                }
                
            }
            return NotFound("DeviceNotFound");
        }
    }
}
