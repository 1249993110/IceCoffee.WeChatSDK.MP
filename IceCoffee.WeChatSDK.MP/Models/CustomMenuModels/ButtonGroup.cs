using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace IceCoffee.WeChatSDK.MP.Models.CustomMenuModels
{
    public class ButtonGroup
    {
        [JsonPropertyName("button")]
        public List<Button> Buttons { get; set; } = new List<Button>();
    }
}
