using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace AzureUrlShortener.Functions.Extensions
{
    public static class GetClientIPAddress
    {
        public static IPAddress ClientIP(this HttpRequest request)
        {
            IPAddress result = null;
            if (request.Headers.TryGetValue("X-Forwarded-For", out StringValues values))
            {
                var ipn = values.FirstOrDefault().Split(new char[] { ',' }).FirstOrDefault().Split(new char[] { ':' }).FirstOrDefault();
                IPAddress.TryParse(ipn, out result);
            }
            if (result == null)
            {
                result = request.HttpContext.Connection.RemoteIpAddress;
            }
            return result;
        }
    }
}
