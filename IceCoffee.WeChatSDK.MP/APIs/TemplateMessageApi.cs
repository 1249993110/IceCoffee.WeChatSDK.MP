using IceCoffee.WeChatSDK.MP.Models;
using IceCoffee.WeChatSDK.MP.Options;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCoffee.WeChatSDK.MP.Apis
{
    public class TemplateMessageApi : BasicApi, ITemplateMessageApi
    {
        public TemplateMessageApi(WeChatMpOpenApiOptions options, IMemoryCache memoryCache, IHttpClientFactory clientFactory) : base(options, memoryCache, clientFactory)
        {
        }

        public async Task<ResponseModelBase> SendAsync(TemplateMessage templateMessage)
        {
            string accessToken = (await GetAccessTokenAsync()).AccessToken;
            string url = "/cgi-bin/message/template/send?access_token=" + accessToken;
            return await base.PostAsync<ResponseModelBase>(url, templateMessage);
        }
    }
}
