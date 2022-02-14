using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace IceCoffee.WeChatSDK.MP.Models.TagModels
{
    public class BatchTagging
    {
        [JsonPropertyName("openid_list")]
        public string[] OpenIds { get; set; }

        [JsonPropertyName("tagid")]
        public string TagId { get; set; }
    }
}
