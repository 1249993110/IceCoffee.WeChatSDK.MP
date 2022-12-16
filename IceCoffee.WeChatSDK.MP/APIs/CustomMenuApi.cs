using IceCoffee.WeChatSDK.MP.Models;
using IceCoffee.WeChatSDK.MP.Models.CustomMenuModels;
using IceCoffee.WeChatSDK.MP.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace IceCoffee.WeChatSDK.MP.Apis
{
    public class CustomMenuApi : BasicApi, ICustomMenuApi
    {
        public CustomMenuApi(WeChatMpOpenApiOptions options, IMemoryCache memoryCache, IHttpClientFactory clientFactory)
            : base(options, memoryCache, clientFactory)
        {
        }

        public CustomMenuApi(IOptions<WeChatMpOpenApiOptions> options, IMemoryCache memoryCache, IHttpClientFactory clientFactory)
            : base(options, memoryCache, clientFactory)
        {
        }

        public async Task<ResponseModelBase> CreateMenuAsync(ButtonGroup buttonGroup)
        {
            string accessToken = (await GetAccessTokenAsync()).AccessToken;
            string url = "/cgi-bin/menu/create?access_token=" + accessToken;

            return await base.PostAsync<ResponseModelBase>(url, buttonGroup);
        }
    }
}