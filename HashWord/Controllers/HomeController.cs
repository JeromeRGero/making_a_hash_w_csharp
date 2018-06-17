using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HashWord.Models;
using Microsoft.AspNetCore.Http;

namespace HashWord.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            // string RandomString(int length)
            if(HttpContext.Session.GetInt32("CurNum") == null){
                HttpContext.Session.SetInt32("CurNum", 0);
            }
            // Using the SetString() method we store username and RandomString() which is a method that we created below. 
            string randomo = RandomString(14);                         
            HttpContext.Session.SetString("RandomHash", randomo);
            Dictionary<string, string> LocalVariables = new Dictionary<string, string>(); 
            LocalVariables.Add("RandomHash", randomo);
            int v1 = (int)HttpContext.Session.GetInt32("CurNum");
            LocalVariables.Add("CurNum", v1.ToString());
            return View("Index", LocalVariables);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            string LocalVariable = HttpContext.Session.GetString("UserName");
            return View("About", LocalVariable);
        }

        [HttpGet]
        [Route("Contact")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            if(HttpContext.Session.GetInt32("CurNum") != null){
                int? temp = HttpContext.Session.GetInt32("CurNum");
                int newtemp = temp.GetValueOrDefault() + 1;
                HttpContext.Session.SetInt32("CurNum", newtemp);
            }else{
                HttpContext.Session.SetInt32("CurNum", 0);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}