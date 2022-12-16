using Microsoft.AspNetCore.Http;

namespace IceCoffee.WeChatSDK.MP.Options
{
    /// <summary>
    /// 微信公众号服务端配置选项
    /// </summary>
    public class WeChatMpServerOptions
    {
        internal Type messageHandlerType;

        /// <summary>
        /// 微信通知地址
        /// </summary>
        public PathString NotifyPath { get; set; }

        public string Token { get; set; }

        public string EncodingAESKey { get; set; }
    }
}