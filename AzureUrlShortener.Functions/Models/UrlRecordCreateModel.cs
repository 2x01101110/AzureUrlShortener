using HashidsNet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AzureUrlShortener.Functions.Models
{
    public class UrlRecordCreateModel
    {
        public string LongUrl { get; }

        public UrlRecordCreateModel()
        {

        }

        [JsonConstructor]
        public UrlRecordCreateModel(string longUrl)
        {
            this.LongUrl = longUrl;
        }
    }
}
