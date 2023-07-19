using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using ProjectKMAIOT.Dto;
using ProjectKMAIOT.Models;
using ProjectKMAIOT.Rules;

namespace ProjectKMAIOT.Hubs
{
    public class SignalRServer : Hub
    {
        private Daily dl = new Daily();
        private Hourly hl = new Hourly();

        public async Task SaveData(string data)
        {
            ReceiveData rcd = JsonConvert.DeserializeObject<ReceiveData>(data);
            await hl.ControlData(rcd);
            await  dl.ControlData(rcd);
            await Clients.All.SendAsync("refreshData");
        }
        public async Task GetOnlyDay()
        {
            DailyDataRecord Onlywd = await dl.GetOnlyDayData();
            await Clients.All.SendAsync("GetOnlyDay",Onlywd);
        }
        public async Task GetDaily()
        {
            List<WeekData> lwd = await dl.GetWeeksData();
            await Clients.All.SendAsync("GetDailyHub", lwd);
        }
        public async Task GetHourly()
        {
            List<HourlyDataRecord> lhd = await hl.GetHoursData();
            await Clients.All.SendAsync("GetHourly",lhd);
        }
    }
}
