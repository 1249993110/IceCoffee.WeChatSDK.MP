using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace IceCoffee.WeChatSDK.MP.Options
{
    /// <summary>
    /// 配置选项
    /// </summary>
    public class WeChatMpOptions
    {
        internal Type messageHandlerType;
        internal PathString notifyPath;

        /// <summary>
        /// 微信通知地址
        /// </summary>
        public PathString NotifyPath { get => notifyPath; set => notifyPath = value; }

        public string Token { get; set; }

        public string EncodingAESKey { get; set; }

        public string WeChatAppId { get; set; }

        public string WeChatAppSecret { get; set; }
    }

    
}
