namespace MaxiCrush.Rest.Exceptions
{
    public class RestHttpRequestException : Exception
    {
        public RestResponseMessage Response { get; set; }

        public RestHttpRequestException(RestResponseMessage response)
            => Response = response;
    }
}
