using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Simply_Watered.Data;
using Simply_Watered.Models;
using Simply_Watered_IOT.Models;

namespace Simply_Watered.Controllers.IOT
{
    [ApiController]
    [Route("api/[controller]")]
    public class IotController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
       

        public IotController( ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{serial}")]
        public async Task<IoTDevice> Get(string serial)
        {
            Devices device = await _context.Devices.Include(e=>e.Region).FirstOrDefaultAsync(e => e.SerialNumber == serial);
            RegionGroups regionGroup =
                await _context.RegionGroups.FirstOrDefaultAsync(e => e.RegionGroupId == device.Region.RegionGroupId);
            IrrigationSchedules schedule = await _context.IrrigationSchedules
                .Include(e=>e.ScheduleTimespans)
                .FirstOrDefaultAsync(e => e.RegionGroupId == regionGroup.RegionGroupId
                                          && e.ScheduleStartDate < DateTime.Now
                                          && e.ScheduleEndDate > DateTime.Now);
            if (schedule == null)
            {

            }

            ScheduleTimespans timespan = schedule.ScheduleTimespans.First();
            IoTSchedule responseSchedule = new IoTSchedule()
            {
                IrrigScheduleId = schedule.IrrigScheduleId,
                IrrigScheduleName = schedule.IrrigScheduleName,
                ScheduleStartDate = schedule.ScheduleStartDate,
                ScheduleEndDate = schedule.ScheduleEndDate,
                Start = timespan.Start,
                Finish = timespan.Finish
            };

            IoTDevice response = new IoTDevice()
            {
                SerialNumber = device.SerialNumber,
                Active = device.Active,
                DeviceId = device.DeviceId,
                IrrigModeId = device.IrrigModeId,
                MaxHumidity = device.MaxHumidity,
                MinimalHumidity = device.MinimalHumidity,
                Schedule = responseSchedule
            };
            return response;
        }


        public class History
        {
          
            public DateTime StartDateTime { get; set; }
          
            public DateTime EndDateTime { get; set; }
            
            public double? ReadingStartTemp { get; set; }
            public double? ReadingStartHumidity { get; set; }
            public double? ReadingEndTemp { get; set; }
            public double? ReadingEndHumidity { get; set; }
            public long DeviceId { get; set; }
            public long? IrrigModeId { get; set; }

        }
        [HttpPost]
        public async Task<IActionResult> Post(History deviceHistory)
        {
            IrrigationHistory irrigationHistory = new IrrigationHistory(
                deviceHistory.StartDateTime,
                deviceHistory.EndDateTime,
                deviceHistory.ReadingStartTemp,
                deviceHistory.ReadingEndTemp,
                deviceHistory.ReadingStartHumidity,
                deviceHistory.ReadingEndHumidity,
                deviceHistory.DeviceId,
                deviceHistory.IrrigModeId);

            await _context.IrrigationHistory.AddAsync(irrigationHistory);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
