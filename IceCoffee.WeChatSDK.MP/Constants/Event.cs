using System;
using System.Collections.Generic;
using System.Text;

namespace IceCoffee.WeChatSDK.MP.Constants
{
    /// <summary>
    /// 微信事件
    /// </summary>
    public struct Event
    {
        /// <summary>
        /// 关注事件
        /// </summary>
        public const string Subscribe = "subscribe";

        /// <summary>
        /// 取消关注
        /// </summary>
        public const string Unsubscribe = "unsubscribe";

        /// <summary>
        /// 自定义菜单点击事件
        /// </summary>
        public const string Click = "CLICK";

        /// <summary>
        /// 点击链接跳转
        /// </summary>
        public const string View = "VIEW";

        /// <summary>
        /// 扫码事件
        /// </summary>
        public const string Scan = "SCAN";

        /// <summary>
        /// 上报地理位置
        /// </summary>
        public const string Location = "LOCATION";
    }
}
