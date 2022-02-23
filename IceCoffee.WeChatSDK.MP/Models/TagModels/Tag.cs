using System.Text.Json.Serialization;

namespace IceCoffee.WeChatSDK.MP.Models.TagModels
{
    public class Tag
    {
        /// <summary>
        /// 此标签下粉丝数
        /// </summary>
        [JsonPropertyName("count")]
        public int Count { get; set; }

        /// <summary>
        /// 标签Id
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// 标签名
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}