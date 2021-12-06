using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Simply_Watered.Data;
using Simply_Watered.Models;
using Simply_Watered.ViewModels;

namespace Simply_Watered.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/regiongroups/{groupId:long}/[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(ILogger<RegionsController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;

        }

       

        [HttpGet]
        public async Task<RegionsViewModel> Get(long groupId)
        {

            IEnumerable<Regions> regions = _context.Regions
                .Include(r => r.Devices)
                .Where(r => r.RegionGroupId == groupId)
                .ToArray();

            RegionGroups regionGroup = await _context.RegionGroups.Where(g => g.RegionGroupId == groupId).FirstOrDefaultAsync();
                IEnumerable<IrrigationModes> modes = await _context.IrrigationModes.ToListAsync();


                IEnumerable<DeviceTypes> types = await _context.DeviceTypes.ToListAsync();


                RegionsViewModel viewModel = new RegionsViewModel()
                {
                    Regions = regions,
                    RegionGroup = regionGroup,
                    Modes=modes,
                    //Types=types
                };

                return viewModel;
                
        }



       
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long groupId, long id)
        {
            
            
                Regions region = _context.Regions.FirstOrDefault(r => r.RegionId == id);
                if (region != null)
                {
                    _context.Regions.Remove(region);
                    await _context.SaveChangesAsync();
                    return Ok(region);
                }

                return NotFound();

        }

        public class InputModel
        {
            public string RegionName { get; set; }
            public string RegionDescription { get; set; }
            //public long GroupId { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> Post(long groupId, [FromBody] InputModel inputModel)
        {
            if (inputModel != null)
            {
                
                Regions newRegion = new Regions
                {
                    RegionName = inputModel.RegionName,
                    RegionDescription = inputModel.RegionDescription,
                    RegionGroupId = groupId

                };
                _context.Regions.Add(newRegion);
                await _context.SaveChangesAsync();
                return Ok(inputModel);
            }
            return BadRequest();
        }


    }
}
