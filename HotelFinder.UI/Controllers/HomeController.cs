using HotelFinder.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HotelFinder.Entities;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace HotelFinder.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string Baseurl = "http://localhost:58529/api/Hotels";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> HotelView()
        {
            //HotelList Hotel = new HotelList();
            List<HotelModel> hotelList = new List<HotelModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync(Baseurl);
                if (Res.IsSuccessStatusCode)
                {
                    var HotelResponse = Res.Content.ReadAsStringAsync().Result;
                    hotelList = JsonConvert.DeserializeObject<List<HotelModel>>(HotelResponse);
                }
            }
            return View(hotelList);
        }
        //GetHotelById
        public async Task<IActionResult> GetHotelById(int id)
        {
            HotelModel hotelModel = new HotelModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl + "/GetHotelById/" + id);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync(Baseurl + "/GetHotelById/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var HotelResponse = Res.Content.ReadAsStringAsync().Result;
                    hotelModel = JsonConvert.DeserializeObject<HotelModel>(HotelResponse);
                }
            }
            return View(hotelModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HotelModel hotelModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl + "/Post/");
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HttpContent content = new StringContent(JsonConvert.SerializeObject(hotelModel), Encoding.UTF8, "application/json");

                var response =  client.PostAsJsonAsync<HotelModel>(client.BaseAddress, hotelModel);
                response.Wait();
                var result = response.Result;
              if(result.IsSuccessStatusCode)
                {
                    var model = result.Content.ReadAsAsync<HotelModel>().Result;//eklediğim veriyi model olarak aldım
                    return View();
                }
            }
            return View();
        }   

        [HttpGet]
        public async Task<IActionResult> Update(int? id)        
        {
            HotelModel hotelModel = new HotelModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl + "/GetHotelById/" + id);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync(Baseurl + "/GetHotelById/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var HotelResponse = Res.Content.ReadAsStringAsync().Result;
                    hotelModel = JsonConvert.DeserializeObject<HotelModel>(HotelResponse);
                }
            }
            return View(hotelModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(HotelModel model)   
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl + "/Put/");
                var response = client.PostAsJsonAsync<HotelModel>(client.BaseAddress, model);
                 response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    return View(model);
                }
            }
            return View(model);
        }
            
        [HttpPost]      
        public async Task<IActionResult> Delete(int id)
        {
            HotelModel hotelModel = new HotelModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl + "/Delete/" + id);
                HttpResponseMessage Res = await client.DeleteAsync(client.BaseAddress);
                if (Res.IsSuccessStatusCode)
                {
                    return View();
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
