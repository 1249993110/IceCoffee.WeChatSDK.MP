using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace IceCoffee.WeChatSDK.MP.Models
{
    /// <summary>
    /// 响应模型基类
    /// </summary>
    public class ResponseModelBase
    {
        /// <summary>
        /// 返回结果信息
        /// </summary>
        [JsonPropertyName("errmsg")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("errcode")]
        public ErrorCode ErrorCode { get; set; }

        [JsonPropertyName("P2PData")]
        public object P2PData { get; set; }

        public override string ToString()
        {
            return string.Format("WxJsonResult：{{errcode:'{0}',errcode_name:'{1}',errmsg:'{2}'}}",
                (int)ErrorCode, ErrorCode.ToString(), ErrorMessage);
        }
    }
}
