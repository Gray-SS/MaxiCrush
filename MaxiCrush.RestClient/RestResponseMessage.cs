using System.Net;
using MaxiCrush.Rest.Exceptions;
using Newtonsoft.Json;

namespace MaxiCrush.Rest
{
    public class RestResponseMessage
    {
        public bool IsSuccess => StatusCode == HttpStatusCode.OK;
        public HttpStatusCode StatusCode => HttpResponse.StatusCode;
        public HttpResponseMessage HttpResponse { get; private set; }

        public RestResponseMessage(HttpResponseMessage response)
        {
            HttpResponse = response;
        }

        public void EnsureStatusCode()
        {
            if (!IsSuccess)
            {
                throw new RestHttpRequestException(this);
            }
        }

        public async Task<T> GetDataAsync<T>()
        {
            var json = await HttpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<string> GetErrorMessageAsync()
            => await HttpResponse.Content.ReadAsStringAsync();
    }
}
