using IceCoffee.WeChatSDK.MP.Models.BasicApiModels;
using IceCoffee.WeChatSDK.MP.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IceCoffee.WeChatSDK.MP.APIs
{
    /// <summary>
    /// 基础支持接口
    /// </summary>
    public class BasicApi : ApiBase, IBasicApi
    {
        public BasicApi(WeChatMpOptions options, IMemoryCache memoryCache, IHttpClientFactory clientFactory) 
            : base(options, memoryCache, clientFactory)
        {
        }

        public BasicApi(IOptions<WeChatMpOptions> options, IMemoryCache memoryCache, IHttpClientFactory clientFactory)
            : base(options, memoryCache, clientFactory)
        {
        }

        public Task<AccessTokenWrapper> GetAccessTokenAsync(string code)
        {
            string uri = string.Format("/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code",
                    Options.WeChatAppId, Options.WeChatAppSecret, code);

            return base.GetAsync<AccessTokenWrapper>(uri);
        }

        public async Task<AccessTokenWrapper> GetAccessTokenAsync()
        {
            // 命名空间:类名:目标
            string key = $"IceCoffee.WeChatSDK.MP:BasicApi:{Options.WeChatAppId}:AccessToken";
            if (MemoryCache.TryGetValue(key, out AccessTokenWrapper cacheEntry) == false)
            {

                string uri = string.Format("/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}",
                    Options.WeChatAppId, Options.WeChatAppSecret);

                cacheEntry = await base.GetAsync<AccessTokenWrapper>(uri);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                {
                    // 设置绝对过期时间，AccessToken 默认 7200 秒过期
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(7000D)
                };

                MemoryCache.Set(key, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }

        public async Task<JsApiTicket> GetJsApiTicketAsync()
        {
            // 命名空间:类名:目标
            string key = $"IceCoffee.WeChatSDK.MP:CommonApi:{Options.WeChatAppId}:JsApiTicket";
            if (MemoryCache.TryGetValue(key, out JsApiTicket cacheEntry) == false)
            {
                string accessToken = (await GetAccessTokenAsync()).AccessToken;
                string uri = string.Format("/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", accessToken);
                cacheEntry = await base.GetAsync<JsApiTicket>(uri);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                {
                    // 设置绝对过期时间，JsApiTicket 默认 7200 秒过期
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(7000D)
                };

                MemoryCache.Set(key, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }
    }
}
