using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mars_rover_models.ViewModels;
using mars_rover_BL.Services;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Net.Http.Headers;

namespace mars_rover_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoldersController : ControllerBase
    {
        private readonly IMarsRoverService _marsRoverService;

        public FoldersController(IMarsRoverService marsRoverService)
        {
            this._marsRoverService = marsRoverService;
        }

        [HttpGet]
        public ActionResult<DateFolders> Get()
        {
            return _marsRoverService.GetDateFolders();
        }

        [HttpGet("{date}")]
        public ActionResult<List<GalleryImages>> Get(string date)
        {
            return _marsRoverService.GetGalleryImages(date);
        }

            //[Route("Image/{id}")]
            //[HttpGet]
            //public HttpResponseMessage GetImage(string id)
            //{
            //    var fileContent = _marsRoverService.GetImageFile(id);

            //    var response = new HttpResponseMessage(HttpStatusCode.OK)
            //    {
            //        Content = new ByteArrayContent(fileContent)
            //    };
            //    return response;
            //}

        [Route("Image/{id}")]
        [HttpGet]
        public ActionResult GetImage(string id)
        {
            var fileContent = _marsRoverService.GetImageFile(id);

            return new FileContentResult(fileContent, "image/jpeg");
        }

        [HttpPost]
        public ActionResult Post([FromBody] string authPassKey)
        {
            _marsRoverService.LoadNewDates(authPassKey);

            return Ok();
        }
    }
}
