using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace mars_rover_BL.Services
{
    public class WebApiService : IWebService
    {
        public async Task<HttpResponseMessage> Get(string url)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage responseMessage = null;
                try
                {
                    responseMessage = await client.GetAsync(url);
                }
                catch (Exception ex)
                {
                    responseMessage = GetExceptionResponseMessage(responseMessage, url, ex);
                }

                return responseMessage;

            }
        }


        public async Task<HttpResponseMessage> Post(string url, HttpContent content)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage responseMessage = null;
                try
                {
                    responseMessage = await client.PostAsync(new Uri(url), content);
                }
                catch (Exception ex)
                {
                    responseMessage = GetExceptionResponseMessage(responseMessage, url, ex);
                }

                return responseMessage;
            }
        }



        /// <summary>
        /// Returns a failure resonse when the response result is not a success.
        /// </summary>
        /// <param name="response">The response that we intend to return to the client</param>
        /// <param name="url">The url used for the invocation</param>
        /// <param name="ex">The exception to wrap and manage</param>
        /// <param name="memberName">Name of the calling method (automatically injected by compiler)</param>
        /// <returns></returns>
        private HttpResponseMessage GetExceptionResponseMessage(
            HttpResponseMessage responseMessage, string url, Exception ex,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            responseMessage = responseMessage ?? new HttpResponseMessage();

            responseMessage.StatusCode = HttpStatusCode.ServiceUnavailable;
            responseMessage.Content = new StringContent($"{memberName}({url}) failed: {ex}");

            return responseMessage;
        }

    }
}
