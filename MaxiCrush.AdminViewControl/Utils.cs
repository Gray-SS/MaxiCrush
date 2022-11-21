using MaxiCrush.Rest.Exceptions;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace MaxiCrush.AdminViewControl;

public static class Utils
{
    public static async Task HandleRequest(Func<Task> request)
    {
        try
        {
            await request();
        }
        catch (RestHttpRequestException exception)
        {
            var response = exception.Response;

            if (response.HttpResponse.Content.Headers.ContentType?.MediaType == "application/problem+json")
            {
                var json = await response.HttpResponse.Content.ReadAsStringAsync();
                MessageBox.Show(json);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                MessageBox.Show($"Something went wrong on the server...");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                MessageBox.Show($"Unsufficient permissions !");
            }
        }
    }
}
