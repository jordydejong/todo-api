using System.Net;

namespace TodoApi.Middleware
{
    public class IpWhitelistMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] _allowedIps;

        public IpWhitelistMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _allowedIps = configuration.GetSection("AllowedIPs").Get<string[]>() ?? Array.Empty<string>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var remoteIp = context.Connection.RemoteIpAddress;

            if (remoteIp != null && !IsIpAllowed(remoteIp))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Access denied");
                return;
            }

            await _next(context);
        }

        private bool IsIpAllowed(IPAddress remoteIp)
        {
            if (_allowedIps.Length == 0) return true; // No restriction if no IPs configured

            return _allowedIps.Any(allowedIp =>
                IPAddress.Parse(allowedIp).Equals(remoteIp) ||
                allowedIp == "127.0.0.1" && IPAddress.IsLoopback(remoteIp));
        }
    }
}