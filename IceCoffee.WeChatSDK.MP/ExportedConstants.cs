using System;
using System.Collections.Generic;
using System.Text;

namespace IceCoffee.WeChatSDK.MP
{
    /// <summary>
    /// Exported constants
    /// </summary>
    public static class ExportedConstants
    {
        /// <summary>
        /// 授权类型
        /// </summary>
        public static class GrantType
        {
            /// <summary>
            /// client_credential
            /// </summary>
            public const string ClientCredential = "client_credential";
        }

        /// <summary>
        /// Cache
        /// </summary>
        public static class Cache
        {
            /// <summary>
            /// 令牌缓存KEY
            /// </summary>
            public const string AccessTokenCacheKey = "ACCESS_TOKEN_CACHE_KEY";
        }

        /// <summary>
        /// 微信事件
        /// </summary>
        public static class Event
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


        /// <summary>
        /// 消息类型
        /// </summary>
        public static class MsgType
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
        /// <summary>
        /// 消息类型
        /// </summary>
        public static class ButtonType
        {
            public const string Click = "click";
            public const string View = "view";
            public const string MiniProgram = "miniprogram";
            public const string ScancodePush = "scancode_push";
            public const string ScancodeWaitMsg = "scancode_waitmsg";
            public const string PicSysphoto = "pic_sysphoto";
            public const string PicPhotoOrAlbum = "pic_photo_or_album";
            public const string PicWeixin = "pic_weixin";
            public const string LocationSelect = "location_select";
            public const string MediaId = "media_id";
            public const string ViewLimited = "view_limited";
        }
    }
}
