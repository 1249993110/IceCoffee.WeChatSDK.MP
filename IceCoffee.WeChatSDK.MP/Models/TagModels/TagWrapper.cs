using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace IceCoffee.WeChatSDK.MP.Models.TagModels
{
    public class TagWrapper : ResponseModelBase
    {
        [JsonPropertyName("tag")]
        public Tag Tag { get; set; }
    }
}
