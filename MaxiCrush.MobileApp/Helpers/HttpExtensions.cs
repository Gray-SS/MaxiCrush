using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiCrush.MobileApp.Extensions
{
    public static class HttpExtensions
    {
        public static async Task<T> ReadDataFromContentAsync<T>(this HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
