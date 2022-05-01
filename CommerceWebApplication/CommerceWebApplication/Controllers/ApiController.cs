using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CommerceWebApplication.Controllers
{
    public class ApiController : Controller
    {
        private string url => "https://data.cityofnewyork.us/resource/w7w3-xahh.json";
        private string app_Token => "oIMPJuMQHIRBjGqnJ7nBSTnMv";
        private int limit => 10;
        [Route("/NY")]
        public IActionResult DataNY()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(
                $"{url}?$limit={limit}&$$app_token={app_Token}&address_state=NY").GetAwaiter().GetResult();
            var str = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return View(JsonConvert.DeserializeObject<List<dynamic>>(str));
        }


        [Route("/NJ")]
        public IActionResult DataNJ()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(
                $"{url}?$limit={limit}&$$app_token={app_Token}&address_state=NJ").GetAwaiter().GetResult();
            var str = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return View(JsonConvert.DeserializeObject<List<dynamic>>(str));
        }

        [Route("/PA")]
        public IActionResult DataPA()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(
                $"{url}?$limit={limit}&$$app_token={app_Token}&address_state=FL").GetAwaiter().GetResult();
            var str = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return View(JsonConvert.DeserializeObject<List<dynamic>>(str));
        }

        [Route("/Chart")]
        public IActionResult Chart()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync($"{url}?$$app_token={app_Token}&address_state=PA").GetAwaiter().GetResult();
            var str = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<List<dynamic>>(str);

            response = client.GetAsync($"{url}?$$app_token={app_Token}&address_state=NJ").GetAwaiter().GetResult();
            str = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            data.AddRange(JsonConvert.DeserializeObject<List<dynamic>>(str));

            response = client.GetAsync($"{url}?$$app_token={app_Token}&address_state=NY").GetAwaiter().GetResult();
            str = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            data.AddRange(JsonConvert.DeserializeObject<List<dynamic>>(str));


            var chartdata = data.GroupBy(g => new {licence= ((DateTime)g.license_creation_date).Year + "", state= g.address_state+""}, (key, g) => new { key = key, count = g.Count() });




            var str1 = JsonConvert.SerializeObject(
                new []
                {
                    new { name= "New York",type="line", data =chartdata.Where(c => c.key.state == "NY").Select(c => c.count) },
                    new { name= "New Jersey",type="line", data =chartdata.Where(c => c.key.state == "NJ").Select(c => c.count) },
                    new { name= "Pennsylvania",type="line", data =chartdata.Where(c => c.key.state == "PA").Select(c => c.count) },
                }
            );
            ViewBag.series = str1;

            var str2 = JsonConvert.SerializeObject(chartdata.Select(c=>c.key.licence).Distinct());
            ViewBag.data = str2;




            return View();
        }
    }
}
