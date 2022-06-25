using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JuegoRPG
{
    public class Name
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("first")]
        public string First { get; set; }

        [JsonPropertyName("last")]
        public string Last { get; set; }
    }

    public class Result
    {
        [JsonPropertyName("name")]
        public Name Name { get; set; }
    }

    public class nombres
    {
        [JsonPropertyName("results")]
        public List<Result> Results { get; set; }
    }
}
