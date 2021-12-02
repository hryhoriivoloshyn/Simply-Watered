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
    [Route("api/regiongroups/{groupId:long}/regions/{regionId:long}/devices/{deviceId:long}/[controller]")]
    public class IrrigationHistoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ReadingsController> _logger;

        public IrrigationHistoryController(ILogger<ReadingsController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;

        }


        [HttpGet]
        public async Task<IrrigationHistoryViewModel> Get(long deviceId)
        {



            try
            {
                IEnumerable<IrrigationHistory> histories =  _context.IrrigationHistory
                    .Where(r => r.DeviceId == deviceId).ToArray();
                IEnumerable<IrrigationModes> mode = await _context.IrrigationModes.ToListAsync();
                Devices device = _context.Devices.FirstOrDefault(d => d.DeviceId == deviceId);
                device.DeviceType = _context.DeviceTypes.FirstOrDefault(t => t.TypeId == device.TypeId);
                IrrigationHistoryViewModel viewModel = new IrrigationHistoryViewModel()
                {

                    IrrigationHistories = histories,
                    Device = device
                };

                return viewModel;
            }catch(Exception e)
            {
                var exc = e;
                return null;
            }
            

           



        }

    }
}
