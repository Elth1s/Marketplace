namespace WebAPI.Helpers
{
    public static class IpUtil
    {
        public static string GetIpAddress(HttpRequest request, HttpContext httpContext)
        {
            // get source ip address for the current request
            if (request.Headers.ContainsKey("X-Forwarded-For"))
                return request.Headers["X-Forwarded-For"];
            else
                return httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
