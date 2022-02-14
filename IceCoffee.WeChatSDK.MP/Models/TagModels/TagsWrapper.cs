using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace IceCoffee.WeChatSDK.MP.Models.TagModels
{
    public class TagsWrapper
    {
        [JsonPropertyName("tags")]
        public Tag[] Tags { get; set; }
    }
}
