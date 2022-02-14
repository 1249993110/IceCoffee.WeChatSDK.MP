using IceCoffee.WeChatSDK.MP.Messages.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace IceCoffee.WeChatSDK.MP.Messages.EventMessages
{
    public class EventMessage : MessageBase
    {
        /// <summary>
        /// 事件
        /// </summary>
        public string Event { get; set; }
    }
}
