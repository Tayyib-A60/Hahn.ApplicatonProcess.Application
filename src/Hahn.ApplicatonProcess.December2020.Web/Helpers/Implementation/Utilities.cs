using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Helpers
{
    public class Utilities: IUtilities
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;
        public Utilities(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Asynchronously make a http call to an endpoint
        /// </summary>
        /// <param name="request"></param>
        /// <param name="baseAddress"></param>
        /// <param name="requestUri"></param>
        /// <param name="method"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> MakeHttpRequest(object request, string baseAddress, string requestUri, HttpMethod method, Dictionary<string, string> headers = null)
        {
            try
            {
                _httpClient = _httpClientFactory.CreateClient();
                _httpClient.BaseAddress = new Uri(baseAddress);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
                if (method == HttpMethod.Post)
                {
                    string data = JsonConvert.SerializeObject(request);
                    HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    return await _httpClient.PostAsync(requestUri, content);
                }
                else if (method == HttpMethod.Get)
                {
                    return await _httpClient.GetAsync(requestUri);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
