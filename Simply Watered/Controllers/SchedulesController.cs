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
    public class SchedulesController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<SchedulesController> _logger;

        public SchedulesController(ILogger<SchedulesController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;

        }

       

        private IEnumerable<IrrigationSchedules> GetSchedulesByGroupId(long groupId)
        {
            long[] regionIds = _context.Regions
                .Where(r => r.RegionGroupId == groupId)
                .Select(r => r.RegionId)
                .ToArray();

            long[] deviceIds = _context.Devices
                .Where(d => regionIds.Contains((long)d.RegionId))
                .Select(d => d.DeviceId)
                .ToArray();

            long[] scheduleIds =
                _context.DevicesSchedules
                    .Where(s => deviceIds.Contains(s.DeviceId))
                    .Select(s => s.ScheduleId)
                    .ToArray();

            IEnumerable<IrrigationSchedules> schedules = _context.IrrigationSchedules
                .Where(i => scheduleIds.Contains(i.IrrigScheduleId)).ToList();

            return schedules;
        }

    
        [HttpGet]
        public async Task<SchedulesViewModel> Get(long groupId)
        {

         
              

                RegionGroups regionGroup = _context.RegionGroups.FirstOrDefault(g => g.RegionGroupId == groupId);



                IEnumerable<IrrigationSchedules> schedules = GetSchedulesByGroupId(groupId);

                foreach (var schedule in schedules)
                {
                    ICollection<ScheduleTimespans> timespans =
                        _context.ScheduleTimespans.Where(t => t.IrrigScheduleId == schedule.IrrigScheduleId).ToList();
                    schedule.ScheduleTimespans = timespans;
                }

                DateTime LastEndDate = schedules.Max(s => s.ScheduleEndDate);
                DateTime minStartDate = DateTime.Now;
                if (LastEndDate > minStartDate)
                {
                    minStartDate = LastEndDate.AddDays(1);
                }

                IEnumerable<IrrigationModes> irrigationModes = _context.IrrigationModes.ToList(); 

                SchedulesViewModel viewModel = new SchedulesViewModel()
                {
                    Schedules = schedules,
                    RegionGroup = regionGroup,
                    IrrigationModes = irrigationModes,
                    MinStartDate = minStartDate.ToString("yyyy-MM-dd"),
                    MinEndDate=minStartDate.AddDays(1).ToString("yyyy-MM-dd")
                };

        
                return viewModel;
            
        }


       
        public class AddModel
        {

            public string ScheduleName { get; set; }
            public string ScheduleStartDate { get; set; }
            public string ScheduleEndDate { get; set; }
            public TimespanModel Timespan { get; set; }

            

            public class TimespanModel
            {
                public string Start { get; set; }
                public string Finish { get; set; }
            }
        }

        public IEnumerable<Devices> GetDevicesByGroupId(long groupId)
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
        [HttpPost]
        public async Task<IActionResult> Post(long groupId, [FromBody] AddModel addModel)
        {
            if (addModel != null)
            {
                DateTime startDate = DateTime.Parse(addModel.ScheduleStartDate);
                DateTime endDate = DateTime.Parse(addModel.ScheduleEndDate);
                TimeSpan startTime = TimeSpan.Parse(addModel.Timespan.Start);
                TimeSpan endTime = TimeSpan.Parse(addModel.Timespan.Finish);

                IrrigationSchedules schedule = new IrrigationSchedules()
                {
                    IrrigScheduleName = addModel.ScheduleName,
                    ScheduleStartDate = startDate,
                    ScheduleEndDate = endDate
                };

                _context.IrrigationSchedules.Add(schedule);

                ScheduleTimespans timespan = new ScheduleTimespans()
                {
                    Start = startTime,
                    Finish = endTime,
                    IrrigSchedule = schedule

                };

                _context.ScheduleTimespans.Add(timespan);

                IEnumerable<Devices> devices = GetDevicesByGroupId(groupId);

                foreach (var device in devices)
                {
                    _context.DevicesSchedules.Add(new DevicesSchedules()
                    {
                        Device = device,
                        Schedule = schedule
                    });
                }

                await _context.SaveChangesAsync();
                return Ok(addModel);
            }
            return NotFound();
        }
    }
}
