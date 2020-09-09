using HashidsNet;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureUrlShortener.Functions.Helpers
{
    public static class HashFromLongUrl
    {
        public static string Create(string longUrl)
        {
            // Hopefully random enough
            var hashids = new Hashids($"{DateTime.UtcNow}{Guid.NewGuid()}", 6);
            var hash = hashids.EncodeLong(longUrl.GetHashCode());
            return hash;
        }
    }
}
