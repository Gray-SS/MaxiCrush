namespace MaxiCrush.Rest;

public struct RestErrorInfo
{
    public string Type { get; set; }
    public string Message { get; set; }

    public RestErrorInfo(string type, string message)
    {
        Type = type;
        Message = message;
    }
}
