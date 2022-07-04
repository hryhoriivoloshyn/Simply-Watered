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

        [HttpGet]
        public async Task<SchedulesViewModel> Get(long groupId)
        {

            RegionGroups regionGroup = _context.RegionGroups.FirstOrDefault(g => g.RegionGroupId == groupId);

            IEnumerable<IrrigationSchedules> schedules = await _context.IrrigationSchedules
                .Where(s => s.RegionGroupId == groupId)
                .ToListAsync();
            DateTime minStartDate = DateTime.Now;

            if (!schedules.Any())
            {
                return new SchedulesViewModel()
                {
                    Schedules = null,
                    RegionGroup = regionGroup,
                    MinStartDate = minStartDate.ToString("yyyy-MM-dd"),
                    MinEndDate = minStartDate.AddDays(1).ToString("yyyy-MM-dd")
                };
            }

            foreach (var schedule in schedules)
            {
                ICollection<ScheduleTimespans> timespans =
                    _context.ScheduleTimespans.Where(t => t.IrrigScheduleId == schedule.IrrigScheduleId).ToList();
                schedule.ScheduleTimespans = timespans;
            }

            DateTime LastEndDate = schedules.Max(s => s.ScheduleEndDate);
        
            if (LastEndDate > minStartDate)
            {
                minStartDate = LastEndDate.AddDays(1);
            }

            

            SchedulesViewModel viewModel = new SchedulesViewModel()
            {
                Schedules = schedules,
                RegionGroup = regionGroup,
                MinStartDate = minStartDate.ToString("yyyy-MM-dd"),
                MinEndDate = minStartDate.AddDays(1).ToString("yyyy-MM-dd")
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
                    ScheduleEndDate = endDate,
                    RegionGroupId = groupId
                };

                _context.IrrigationSchedules.Add(schedule);

                ScheduleTimespans timespan = new ScheduleTimespans()
                {
                    Start = startTime,
                    Finish = endTime,
                    IrrigSchedule = schedule

                };

                _context.ScheduleTimespans.Add(timespan);

               

                await _context.SaveChangesAsync();
                return Ok(addModel);
            }
            return NotFound();
        }

        [HttpDelete("{scheduleId:long}")]
        public async Task<IActionResult> Delete(long groupId, long scheduleId)
        {
            IrrigationSchedules schedule =
                await _context.IrrigationSchedules.FirstOrDefaultAsync(s => s.IrrigScheduleId == scheduleId);
            if (schedule != null)
            {
                _context.Remove(schedule);
                await _context.SaveChangesAsync();
                return Ok(scheduleId);
            }
            
            return NotFound();
        }
    }
}
