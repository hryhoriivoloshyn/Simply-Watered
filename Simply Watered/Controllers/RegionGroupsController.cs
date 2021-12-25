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
using Simply_Watered.Services;
using Simply_Watered.ViewModels;

namespace Simply_Watered.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RegionGroupsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegionGroupsController> _logger;
        

        public RegionGroupsController(ILogger<RegionGroupsController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
           
        }



        public class Test
        {
           
            public string userid { get; set; }

        }
        [HttpGet]
        public IEnumerable<RegionGroups> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var userId = "839c3a06-9e5e-4664-8a65-7b97b6f8ea97";
            IEnumerable<RegionGroups> groups = _context.RegionGroups.Where(g => g.UserId == userId).ToArray();
            return groups;
            //Test test1 = new Test()
            //{

            //    userid = "First"
            //};

            //Test test2 = new Test()
            //{

            //    userid = "Second"
            //};

            //List<Test> tests= new List<Test>();
            //tests.Add(test1);
            //tests.Add(test2);

            //return tests;
        }


       

        public class InputModel
        {
            public string GroupName { get; set; }
            public string RegionGroupDescription { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InputModel inputModel)
        {
            if (inputModel != null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return NotFound($"Unable to load user with ID '{userId}'.");
                }
                RegionGroups newGroup = new RegionGroups
                {
                    GroupName = inputModel.GroupName,
                    RegionGroupDescription = inputModel.RegionGroupDescription,
                    UserId = userId

                };
                _context.RegionGroups.Add(newGroup);
                await _context.SaveChangesAsync();
                return Ok(inputModel);
            }
            return NotFound();
        }

        public class PutModel
        {
            public long ModeId { get; set; }
            public int MinHumidity { get; set; }
            public int MaxHumidity { get; set; }
        }
        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateGroupSettings(long id, [FromBody] PutModel inputModel)
        {
            IEnumerable<Devices> devices = DeviceManager.GetDevicesByGroupId(id, _context);
            if (devices != null)
            {
                devices.Select(d =>
                {
                    d.IrrigModeId = inputModel.ModeId;
                    d.MinimalHumidity = inputModel.MinHumidity;
                    d.MaxHumidity = inputModel.MaxHumidity;
                    return d;
                }).ToList();
                _context.Devices.UpdateRange(devices);
                await _context.SaveChangesAsync();
                return Ok(inputModel);
            }
            
            return BadRequest();
        }

        [HttpDelete("{Id:long}")]
        public async Task<IActionResult> Delete(long Id)
        {
            
          
            
            
                RegionGroups group = _context.RegionGroups.FirstOrDefault(g => g.RegionGroupId == Id);
                if (group != null)
                {
                    _context.RegionGroups.Remove(group);
                    await _context.SaveChangesAsync();
                    return Ok(Id);
                }

                return NotFound();
            
        }

        
       
    }
}
