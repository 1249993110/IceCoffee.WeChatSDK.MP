using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace IceCoffee.WeChatSDK.MP.Models.CustomMenuModels
{
    public class Button
    {
        [JsonPropertyName("sub_button")]
        public List<Button> SubButtons { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("media_id")]
        public string MediaId { get; set; }

        [JsonPropertyName("appid")]
        public string AppId { get; set; }

        [JsonPropertyName("pagepath")]
        public string PagePath { get; set; }
    }
}
