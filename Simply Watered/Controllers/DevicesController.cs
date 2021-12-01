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
    [Route("api/regiongroups/{groupId:long}/regions/{regionId:long}/[controller]")]
    public class DevicesController : ControllerBase
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
        [HttpGet]
        public async Task<DevicesViewModel> Get(long regionId)
        {
           
                
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
               
                return viewModel;
            

           
        }

       
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {

            
          
               
                Devices device = _context.Devices.FirstOrDefault(r => r.DeviceId == id);
                if (device != null)
                {
                    device.RegionId = null;
                    _context.Devices.Update(device);
                    await _context.SaveChangesAsync();
                    return Ok(id);
                }

                return BadRequest();



        }

        public class InputModel
        {
            
            public string SerialNumber { get; set; }
        }
        [HttpPut]
        public async Task<IActionResult> AddDevice(long regionId, [FromBody] InputModel inputModel)
        {
            if (inputModel != null)
            {
                
                var serialNumber = inputModel.SerialNumber;
             
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
