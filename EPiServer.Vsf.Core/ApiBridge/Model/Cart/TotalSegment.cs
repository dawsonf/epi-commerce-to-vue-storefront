﻿using Newtonsoft.Json;

namespace EPiServer.Vsf.Core.ApiBridge.Model.Cart
{
    public class TotalSegment
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("value")]
        public decimal? Value { get; set; }

        [JsonProperty("extension_attributes", NullValueHandling = NullValueHandling.Ignore)]
        public TotalSegmentExtensionAttributes ExtensionAttributes { get; set; }

        [JsonProperty("area", NullValueHandling = NullValueHandling.Ignore)]
        public string Area { get; set; }
    }
}
