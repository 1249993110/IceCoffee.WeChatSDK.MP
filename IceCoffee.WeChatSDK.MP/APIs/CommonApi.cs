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
