using Hahn.ApplicatonProcess.December2020.Domain.Helpers;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Helpers
{
    public interface IUtilities
    {
        Task<HttpResponseMessage> MakeHttpRequest(object request, string baseAddress, string requestUri, HttpMethod method, Dictionary<string, string> headers = null);
    }
}
