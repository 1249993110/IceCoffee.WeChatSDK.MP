
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace IceCoffee.WeChatSDK.MP.Models.BasicApiModels
{
    public class Resp_JsApiTicket : ResponseModelBase
    {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        [JsonPropertyName("ticket")]
        public string Ticket { get; set; }

        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
