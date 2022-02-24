
using System;
using System.Collections.Generic;
using System.Text;

namespace IceCoffee.WeChatSDK.MP.Messages.CommonMessages
{
    /// <summary>
    /// 图文消息
    /// </summary>
    public class ArticlesMessage : MessageBase
    {
        /// <summary>
        /// 图文消息个数；当用户发送文本、图片、语音、视频、图文、地理位置这六种消息时，开发者只能回复1条图文消息；其余场景最多可回复8条图文消息
        /// </summary>
        public int ArticleCount { get; set; }

        public Articles Articles { get; set; }
    }

    public class Articles
    {
        public List<ArticleItem> Item { get; set; }
    }

    public class ArticleItem
    {
        /// <summary>
        /// 图文消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图文消息描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200
        /// </summary>
        public int PicUrl { get; set; }

        /// <summary>
        /// 点击图文消息跳转链接
        /// </summary>
        public string Url { get; set; }

    }
}
