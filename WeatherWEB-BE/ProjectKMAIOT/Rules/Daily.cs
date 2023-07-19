using Microsoft.EntityFrameworkCore;
using ProjectKMAIOT.Dto;
using ProjectKMAIOT.Models;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;

namespace ProjectKMAIOT.Rules
{
    public class Daily
    {
        private MyDbConnect db = new MyDbConnect();
        public string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

        public async Task<List<WeekData>> GetWeeksData()
        {
             List<WeekData> ls = await db.DailyRecord.OrderByDescending(x => x.ID).Take(7).Select(x => new WeekData { Day = x.Day, TemperatureMax = x.TemperatureMax, RainSensor = x.RainSensor,ID=x.ID}).ToListAsync();    
            return ls;  
        }
        public async Task<DailyDataRecord> GetOnlyDayData()
        {
            DailyDataRecord ls = await db.DailyRecord.OrderByDescending(x => x.ID).FirstOrDefaultAsync();
            return ls;
        }


        public async Task ControlData(ReceiveData rd)
        {
            DailyDataRecord RecordLast = await db.DailyRecord.OrderByDescending(x => x.ID).FirstOrDefaultAsync();
            if (RecordLast == null || RecordLast.Day.Equals(currentDate) == false)
            {
                await CreateRecord(rd);
            }
            else
            {
                await UpdateRecord(rd, RecordLast);
            }

        }

        public async Task CreateRecord(ReceiveData rd)
        {
            DailyDataRecord record = new DailyDataRecord();
            record.TemperatureMin = rd.temperature;
            record.TemperatureMax = rd.temperature; 
            record.HumidityMin = rd.humidity;   
            record.HumidityMax = rd.humidity;
            record.Day = currentDate;
            record.AirSensor = rd.air;
            record.RainSensor = rd.rain;
            db.DailyRecord.Add(record);
            await db.SaveChangesAsync();   
        }

        public async Task UpdateRecord(ReceiveData rd, DailyDataRecord daily)
        {
            if(daily.TemperatureMin > rd.temperature) { 
                daily.TemperatureMin = rd.temperature;  
            }
            if (daily.TemperatureMax < rd.temperature)
            {
                daily.TemperatureMax = rd.temperature;
            }
            if (daily.HumidityMax < rd.humidity)
            {
                daily.HumidityMax = rd.humidity;
            }
            if (daily.HumidityMin > rd.humidity)
            {
                daily.HumidityMin = rd.humidity;
            }
            daily.AirSensor = rd.air;
            daily.RainSensor = rd.rain;
            await db.SaveChangesAsync();
        }

    }
}
