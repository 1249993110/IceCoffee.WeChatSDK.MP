using IceCoffee.WeChatSDK.MP.Models;
using IceCoffee.WeChatSDK.MP.Models.CustomMenuModels;

using IceCoffee.WeChatSDK.MP.Models.TagModels;
using IceCoffee.WeChatSDK.MP.Options;
using IceCoffee.Common;
using IceCoffee.Common.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using IceCoffee.WeChatSDK.MP.Models.BasicApiModels;

namespace IceCoffee.WeChatSDK.MP.APIs
{
    public class CommonApi : ICommonApi
    {
        private readonly WeChatMpOptions _options;
        private readonly IMemoryCache _memoryCache;

        public CommonApi(WeChatMpOptions options, IMemoryCache memoryCache)
        {
            _options = options;
            _memoryCache = memoryCache;
        }

        public CommonApi(IOptions<WeChatMpOptions> options, IMemoryCache memoryCache)
        {
            _options = options.Value;
            _memoryCache = memoryCache;
        }

        public Resp_AccessToken GetAccessToken(string code)
        {
            return GetAccessTokenAsync(code).Result;
        }

        public async Task<Resp_AccessToken> GetAccessTokenAsync(string code)
        {
            var accessTokenModel =  await HttpHelper.Default.GetObjectAsync<Resp_AccessToken>(
                     string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code",
                     _options.WeChatAppId, _options.WeChatAppSecret, code));

            if (string.IsNullOrEmpty(accessTokenModel.OpenId) == false)
            {
                throw new Exception("获取 OpenId 失败，错误信息：" + accessTokenModel.ToJson());
            }

            return accessTokenModel;
        }

        public Resp_AccessToken GetAccessToken()
        {
            return GetAccessTokenAsync().Result;
        }

        public async Task<Resp_AccessToken> GetAccessTokenAsync()
        {
            // 命名空间:类名:目标
            string key = "IceCoffee.WeChatSDK.MP:CommonApi:AccessToken";
            if (_memoryCache.TryGetValue(key, out Resp_AccessToken cacheEntry) == false)
            {
                cacheEntry = await HttpHelper.Default.GetObjectAsync<Resp_AccessToken>(
                    string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}",
                    _options.WeChatAppId, _options.WeChatAppSecret));

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    // 设置绝对过期时间，AccessToken 默认 7200 秒过期
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(7000D)
                };

                _memoryCache.Set(key, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }

        public Resp_JsApiTicket GetJsApiTicket()
        {
            return GetJsApiTicketAsync().Result;
        }

        public async Task<Resp_JsApiTicket> GetJsApiTicketAsync()
        {
            // 命名空间+目标
            string key = "IceCoffee.WeChatSDK.MP:CommonApi:JsApiTicket";
            if (_memoryCache.TryGetValue(key, out Resp_JsApiTicket cacheEntry) == false)
            {
                string accessToken = (await GetAccessTokenAsync()).AccessToken;

                cacheEntry = await HttpHelper.Default.GetObjectAsync<Resp_JsApiTicket>(
                    string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", accessToken));

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    // 设置绝对过期时间，JsApiTicket 默认 7200 秒过期
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(7000D)
                };

                _memoryCache.Set(key, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }

        public ResponseModelBase CreateMenu(ButtonGroup buttonGroup)
        {
            return CreateMenuAsync(buttonGroup).Result;
        }

        public async Task<ResponseModelBase> CreateMenuAsync(ButtonGroup buttonGroup)
        {
            var accessTokenModel = await GetAccessTokenAsync();
            string url = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + accessTokenModel.AccessToken;
            string json = System.Text.Json.JsonSerializer.Serialize(buttonGroup, new System.Text.Json.JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping// 不进行 unicode编码，微信不支持/u....格式
            });

            return await HttpHelper.Default.PostObjectAsync<ResponseModelBase>(url, json);
        }

        public Tag CreateTag(Tag tagEntry)
        {
            return CreateTagAsync(tagEntry).Result;
        }

        public async Task<Tag> CreateTagAsync(Tag tagEntry)
        {
            var accessTokenModel = await GetAccessTokenAsync();
            string url = "https://api.weixin.qq.com/cgi-bin/tags/create?access_token=" + accessTokenModel.AccessToken;
            string json = System.Text.Json.JsonSerializer.Serialize(tagEntry, new System.Text.Json.JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping// 不进行 unicode编码，微信不支持/u....格式
            });

            return await HttpHelper.Default.PostObjectAsync<Tag>(url, json);
        }


        public TagsWrapper GetTags()
        {
            return GetTagsAsync().Result;
        }

        public async Task<TagsWrapper> GetTagsAsync()
        {
            var accessTokenModel = await GetAccessTokenAsync();
            string url = "https://api.weixin.qq.com/cgi-bin/tags/get?access_token=" + accessTokenModel.AccessToken;
            return await HttpHelper.Default.GetObjectAsync<TagsWrapper>(url);
        }

        public ResponseModelBase UpdateTag(Tag tagEntry)
        {
            return UpdateTagAsync(tagEntry).Result;
        }

        public async Task<ResponseModelBase> UpdateTagAsync(Tag tagEntry)
        {
            var accessTokenModel = await GetAccessTokenAsync();
            string url = "https://api.weixin.qq.com/cgi-bin/tags/update?access_token=" + accessTokenModel.AccessToken;
            string json = System.Text.Json.JsonSerializer.Serialize(tagEntry, new System.Text.Json.JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping// 不进行 unicode编码，微信不支持/u....格式
            });

            return await HttpHelper.Default.PostObjectAsync<ResponseModelBase>(url, json);
        }

        public ResponseModelBase DeleteTag(Tag tagEntry)
        {
            return DeleteTagAsync(tagEntry).Result;
        }

        public async Task<ResponseModelBase> DeleteTagAsync(Tag tagEntry)
        {
            var accessTokenModel = await GetAccessTokenAsync();
            string url = "https://api.weixin.qq.com/cgi-bin/tags/delete?access_token=" + accessTokenModel.AccessToken;
            string json = System.Text.Json.JsonSerializer.Serialize(tagEntry, new System.Text.Json.JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping// 不进行 unicode编码，微信不支持/u....格式
            });

            return await HttpHelper.Default.PostObjectAsync<ResponseModelBase>(url, json);
        }

        public ResponseModelBase BatchTagging(BatchTagging batchTagging)
        {
            return BatchTaggingAsync(batchTagging).Result;
        }

        public async Task<ResponseModelBase> BatchTaggingAsync(BatchTagging batchTagging)
        {
            var accessTokenModel = await GetAccessTokenAsync();
            string url = "https://api.weixin.qq.com/cgi-bin/tags/members/batchtagging?access_token=" + accessTokenModel.AccessToken;
            string json = System.Text.Json.JsonSerializer.Serialize(batchTagging, new System.Text.Json.JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping// 不进行 unicode编码，微信不支持/u....格式
            });

            return await HttpHelper.Default.PostObjectAsync<ResponseModelBase>(url, json);
        }

        public ResponseModelBase BatchUnTagging(BatchTagging batchTagging)
        {
            return BatchUnTaggingAsync(batchTagging).Result;
        }

        public async Task<ResponseModelBase> BatchUnTaggingAsync(BatchTagging batchTagging)
        {
            var accessTokenModel = await GetAccessTokenAsync();
            string url = "https://api.weixin.qq.com/cgi-bin/tags/members/batchuntagging?access_token=" + accessTokenModel.AccessToken;
            string json = System.Text.Json.JsonSerializer.Serialize(batchTagging, new System.Text.Json.JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping// 不进行 unicode编码，微信不支持/u....格式
            });

            return await HttpHelper.Default.PostObjectAsync<ResponseModelBase>(url, json);
        }

        public TagIdList GetUserTags(string openId)
        {
            return GetUserTagsAsync(openId).Result;
        }

        public async Task<TagIdList> GetUserTagsAsync(string openId)
        {
            var accessTokenModel = await GetAccessTokenAsync();
            string url = "https://api.weixin.qq.com/cgi-bin/tags/getidlist?access_token=" + accessTokenModel.AccessToken;
            string json = System.Text.Json.JsonSerializer.Serialize(new { openid = openId }, new System.Text.Json.JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping// 不进行 unicode编码，微信不支持/u....格式
            });

            return await HttpHelper.Default.PostObjectAsync<TagIdList>(url, json);
        }
    }
}
