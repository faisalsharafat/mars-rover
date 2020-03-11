﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mars_rover_web.Models;
using mars_rover_models.DTO;
using Microsoft.Extensions.Options;
using System.Net.Http;
using mars_rover_BL.Services;
using mars_rover_models.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace mars_rover_web.Controllers
{
    public class GalleryController : Controller
    {
        private readonly WepAppSettings _appSettings;
        private readonly IWebService webApiService;

        public GalleryController(IOptions<WepAppSettings> appSettings, IWebService webApiService)
        {
            this._appSettings = appSettings.Value;
            this.webApiService = webApiService;
        }

        public IActionResult Index()
        {
            var response = webApiService.Get($"{_appSettings.BusinessLayerUrl}Folders").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<DateFolders>().Result;
                return View("Index", result);
            }
            return View();
        }
        public IActionResult Gallery(string date)
        {
            List<GalleryImages> model = null;
            var response = webApiService.Get($"{_appSettings.BusinessLayerUrl}Folders/{date}").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<GalleryImages>>(jsonString);

                return View("Gallery", model);
            }
            return View();
        }
        public IActionResult Download(string id)
        {
            var response = webApiService.Get($"{_appSettings.BusinessLayerUrl}Folders/Image/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return new FileContentResult(response.Content.ReadAsByteArrayAsync().Result, "image/jpeg");
            }

            return RedirectToAction("Index", "Gallaery");
        }


        [HttpPost]
        public IActionResult LoadDatesFile([FromBody] string authKey)
        {
            var response = webApiService.Post($"{_appSettings.BusinessLayerUrl}Folders", new StringContent($"\"{authKey}\"", Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }

            return BadRequest();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
