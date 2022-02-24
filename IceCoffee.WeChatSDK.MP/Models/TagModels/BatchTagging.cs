using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace IceCoffee.WeChatSDK.MP.Models.TagModels
{
    public class BatchTagging
    {
        /// <summary>
        /// 粉丝列表
        /// </summary>
        [JsonPropertyName("openid_list")]
        public string[] OpenIds { get; set; }

        /// <summary>
        /// 标签Id
        /// </summary>
        [JsonPropertyName("tagid")]
        public string TagId { get; set; }
    }
}
