using System.Text.Json.Serialization;

namespace IceCoffee.WeChatSDK.MP.Models
{
    public class Miniprogram
    {
        /// <summary>
        /// 所需跳转到的小程序appid（该小程序appid必须与发模板消息的公众号是绑定关联关系, 暂不支持小游戏）
        /// </summary>
        [JsonPropertyName("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 所需跳转到小程序的具体页面路径, 支持带参数,（示例index?foo=bar）, 要求该小程序已发布, 暂不支持小游戏
        /// </summary>
        [JsonPropertyName("pagepath")]
        public string PagePath { get; set; }
    }

    public class TemplateMessage
    {
        /// <summary>
        /// 接收者openid
        /// </summary>
        [JsonPropertyName("touser")]
        public string Touser { get; set; }

        /// <summary>
        /// 模板ID
        /// </summary>
        [JsonPropertyName("template_id")]
        public string TemplateId { get; set; }

        /// <summary>
        /// 模板跳转链接（海外账号没有跳转能力）
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// 跳小程序所需数据, 不需跳小程序可不用传该数据
        /// </summary>
        [JsonPropertyName("miniprogram")]
        public Miniprogram Miniprogram { get; set; }

        /// <summary>
        /// 防重入id。对于同一个openid + client_msg_id, 只发送一条消息,10分钟有效,超过10分钟不保证效果。若无防重入需求, 可不填
        /// </summary>
        [JsonPropertyName("client_msg_id")]
        public string CientMessageId { get; set; }

        /// <summary>
        /// 模板数据
        /// </summary>
        [JsonPropertyName("data")]
        public KeyValuePair<string, string> Data { get; set; }
    }
}