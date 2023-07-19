using Microsoft.AspNetCore.Mvc;
using ProjectKMAIOT.Models;
using System.Text;
using System;
using Newtonsoft.Json;
using ProjectKMAIOT.Dto;
using Microsoft.AspNetCore.SignalR;
using ProjectKMAIOT.Hubs;
using ProjectKMAIOT.Rules;

using OpenAI_API.Completions;
using Microsoft.Identity.Client;

namespace ProjectKMAIOT.Controllers
{
    public class HomeController : Controller
    {

        private Daily _daily = new Daily();
        private Hourly _hourly = new Hourly();
        private OpenAI _openAI = new OpenAI();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetResultOpenAI([FromBody] RequestAI js)
        {
            if (js == null)
            {
                return BadRequest("Missing parameter value");
            }
            string result = _openAI.UseChatGPT(js.body);
            return Json(result);
        }
       
   
       
    }
}
