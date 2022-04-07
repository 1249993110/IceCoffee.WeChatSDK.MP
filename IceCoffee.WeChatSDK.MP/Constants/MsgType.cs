using System;
using System.Collections.Generic;
using System.Text;

namespace IceCoffee.WeChatSDK.MP.Constants
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public struct MsgType
    {
        /// <summary>
        /// 事件
        /// </summary>
        public const string Event = "event";

        /// <summary>
        /// 文本消息
        /// </summary>
        public const string Text = "text";

        /// <summary>
        /// 图片消息
        /// </summary>
        public const string Image = "image";

        /// <summary>
        /// 视频消息
        /// </summary>
        public const string Video = "video";

        /// <summary>
        /// 语音消息
        /// </summary>
        public const string Voice = "voice";

        /// <summary>
        /// 小视频
        /// </summary>
        public const string ShortVideo = "shortvideo";

        /// <summary>
        /// 地理位置
        /// </summary>
        public const string Location = "location";

        /// <summary>
        /// 链接消息
        /// </summary>
        public const string Link = "link";

    }
}
