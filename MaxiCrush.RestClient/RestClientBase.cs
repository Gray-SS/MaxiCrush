using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace MaxiCrush.Rest
{
    public enum RestHttpRequestMethod
    {
        Get,
        Post,
        Put,
        Delete,
        Patch,
    }

    public abstract class RestClientBase
    {
        protected HttpClient HttpClient => _httpClient; 
        private HttpClient _httpClient;
        
        public delegate Task<HttpResponseMessage> HttpRequestMethodDelegate(HttpClient httpClient, Uri? requestUri, HttpContent? content, CancellationToken cancellationToken);

        private readonly Dictionary<RestHttpRequestMethod, HttpRequestMethodDelegate> _requestDelegatesDictionary = new()
        {
            { RestHttpRequestMethod.Get, (client, request, content, cancellationToken) => client.GetAsync(request, cancellationToken) },
            { RestHttpRequestMethod.Post, (client, request, content, cancellationToken) =>  client.PostAsync(request, content, cancellationToken) },
            { RestHttpRequestMethod.Put, (client, request, content, cancellationToken) => client.PutAsync(request, content, cancellationToken ) },
            { RestHttpRequestMethod.Patch, (client, request, content, cancellationToken) => client.PatchAsync(request, content, cancellationToken ) },
            { RestHttpRequestMethod.Delete, (client, request, content, cancellationToken) => client.DeleteAsync(request, cancellationToken ) }
        };

        public enum HostType
        {
            Localhost,
            Azure
        }

        public RestClientBase(HostType hostType)
        {
            Uri baseAddress = hostType switch
            {
                HostType.Localhost => new Uri("https://localhost:7066"),
                HostType.Azure => new Uri("https://maxicrushwapi20221019205107.azurewebsites.net"),
                _ => throw new NotImplementedException()
            };

            _httpClient = new HttpClient
            {
                BaseAddress = baseAddress
            };

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected async Task<RestResponseMessage> DoAsync<TPayload>(RestHttpRequestMethod method, string? requestUri, TPayload? payload, CancellationToken? cancellationToken = null)
            => await DoAsync(method, new Uri(requestUri, UriKind.Relative), payload, cancellationToken);

        protected async Task<RestResponseMessage> DoAsync(RestHttpRequestMethod method, string? requestUri, CancellationToken? cancellationToken = null)
            => await DoAsync<object>(method, requestUri, null, cancellationToken);

        protected async Task<RestResponseMessage> DoAsync(RestHttpRequestMethod method, Uri? requestUri, CancellationToken? cancellationToken = null)
            => await DoAsync<object>(method, requestUri, null, cancellationToken);

        protected async Task<RestResponseMessage> DoAsync<TPayload>(RestHttpRequestMethod method, Uri? requestUri, TPayload? payload, CancellationToken? cancellationToken = null)
        {
            if ((method == RestHttpRequestMethod.Get ||
                method == RestHttpRequestMethod.Delete) &&
                payload != null)
            {
                throw new Exception("Invalid request. GET or DELETE can't have a payload");
            }

            var jsonContent = JsonConvert.SerializeObject(payload);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            requestUri = new Uri(_httpClient.BaseAddress!, requestUri);

            if (!_requestDelegatesDictionary.TryGetValue(method, out HttpRequestMethodDelegate del))
                throw new NotImplementedException();

            var httpResponse = await del.Invoke(_httpClient, requestUri, stringContent, cancellationToken.GetValueOrDefault());

            var response = new RestResponseMessage(httpResponse);
            response.EnsureStatusCode();

            return response;
        }
    }
}
