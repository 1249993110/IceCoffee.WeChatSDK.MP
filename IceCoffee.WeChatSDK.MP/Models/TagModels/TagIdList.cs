using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace IceCoffee.WeChatSDK.MP.Models.TagModels
{
    public class TagIdList : ResponseModelBase
    {
        /// <summary>
        /// //被置上的标签Id列表
        /// </summary>
        [JsonPropertyName("tagid_list")]
        public int[] TagIds { get; set; }
    }
}
