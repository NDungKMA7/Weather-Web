using Microsoft.EntityFrameworkCore;
using ProjectKMAIOT.Dto;
using ProjectKMAIOT.Models;

namespace ProjectKMAIOT.Rules
{
    public class Hourly
    {
        private MyDbConnect db = new MyDbConnect();
        public string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        public string currentHour = DateTime.Now.Hour.ToString();

        public async Task<List<HourlyDataRecord>> GetHoursData()
        {
            List<HourlyDataRecord> hl = await db.HourRecord.ToListAsync();
            return hl;
        }
       
        public async Task ControlData(ReceiveData rd)
        {
            HourlyDataRecord RecordLast = db.HourRecord.OrderByDescending(x => x.ID).FirstOrDefault();
            if (RecordLast == null || RecordLast.Day.Equals(currentDate) == false)
            {
                await RemoveAll();
                await CreateRecord(rd);
            }
            else
            {
                if (RecordLast.Hour.Equals(currentHour))
                {
                    await UpdateRecord(rd, RecordLast);
                }
                else
                {
                    await CreateRecord(rd);
                }
            }

        }

        public async Task RemoveAll()
        {
            List<HourlyDataRecord> hl = db.HourRecord.ToList();
            db.HourRecord.RemoveRange(hl);
            await db.SaveChangesAsync();   
        }

        public async Task CreateRecord(ReceiveData rd)
        {
            HourlyDataRecord record = new HourlyDataRecord();
            record.Temperature = rd.temperature;
            record.Humidity = rd.humidity;
            record.Day = currentDate;
            record.Hour = currentHour;
            record.Air = rd.air;
            record.Rain = rd.rain;
            db.HourRecord.Add(record);
            await db.SaveChangesAsync();
        }

        public async Task UpdateRecord(ReceiveData rd, HourlyDataRecord hourly)
        {

            hourly.Humidity = rd.humidity;
            hourly.Temperature = rd.temperature;
            hourly.Air = rd.air;
            hourly.Rain = rd.rain;
            await db.SaveChangesAsync();
        }

    }
}
