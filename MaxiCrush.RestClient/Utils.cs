using MaxiCrush.Rest.Exceptions;
using Newtonsoft.Json.Linq;

namespace MaxiCrush.Rest;

public static class Utils
{
    public static async Task<RestErrorInfo?> HandleRequest(Func<Task> request)
    {
        try
        {
            await request();
            return null;
        }
        catch (RestHttpRequestException exception)
        {
            var response = exception.Response;

            if (response.HttpResponse.Content.Headers.ContentType?.MediaType == "application/problem+json")
            {
                var json = await response.HttpResponse.Content.ReadAsStringAsync();
                var jtoken = JToken.Parse(json);

                return new RestErrorInfo(string.Empty, jtoken.Value<string>("title") ?? string.Empty);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return new RestErrorInfo("App.Internal", $"Something went wrong on the server...");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return new RestErrorInfo("Permissions.UnsufficientPermission", $"Unsufficient permissions !");
            }

            return new RestErrorInfo("App.Unexpected", string.Empty);
        }
    }
}
