using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace mars_rover_models.ViewModels
{
    public class UploadedFile
    {
        public string AuthKey { get; set; }

        public IFormFile DatesFile { get; set; }
    }
}
