using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace mars_rover_BL.Services
{
    public interface IWebService
    {
        Task<HttpResponseMessage> Get(string url);
        Task<HttpResponseMessage> Post(string url, HttpContent content);
    }
}
