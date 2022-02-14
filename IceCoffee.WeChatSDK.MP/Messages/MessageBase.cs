using System;
using System.Collections.Generic;
using System.Text;

namespace IceCoffee.WeChatSDK.MP.Messages
{
    /// <summary>
    /// Common message base
    /// </summary>
    public class MessageBase
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间（整型)
        /// </summary>
        public long CreateTime { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        public long MsgId { get; set; }
    }
}
