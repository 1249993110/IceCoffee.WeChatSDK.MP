using System;
using IceCoffee.WeChatSDK.MP.Models.BasicApiModels;
using IceCoffee.WeChatSDK.MP.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IceCoffee.WeChatSDK.MP.Models;
using IceCoffee.WeChatSDK.MP.Models.TagModels;

namespace IceCoffee.WeChatSDK.MP.Apis
{
    public class UserManagementApi : BasicApi, IUserManagementApi
    {
        public UserManagementApi(WeChatMpOptions options, IMemoryCache memoryCache, IHttpClientFactory clientFactory)
            : base(options, memoryCache, clientFactory)
        {
        }

        public UserManagementApi(IOptions<WeChatMpOptions> options, IMemoryCache memoryCache, IHttpClientFactory clientFactory)
            : base(options, memoryCache, clientFactory)
        {
        }

        public async Task<ResponseModelBase> BatchTaggingAsync(BatchTagging batchTagging)
        {
            string accessToken = (await GetAccessTokenAsync()).AccessToken;
            string url = "/cgi-bin/tags/members/batchtagging?access_token=" + accessToken;
            return await base.PostAsync<ResponseModelBase>(url, batchTagging);
        }

        public async Task<ResponseModelBase> BatchUnTaggingAsync(BatchTagging batchTagging)
        {
            string accessToken = (await GetAccessTokenAsync()).AccessToken;
            string url = "/cgi-bin/tags/members/batchuntagging?access_token=" + accessToken;
            return await base.PostAsync<ResponseModelBase>(url, batchTagging);
        }

        public async Task<TagWrapper> CreateTagAsync(TagWrapper tagWrapper)
        {
            string accessToken = (await GetAccessTokenAsync()).AccessToken;
            string url = "/cgi-bin/tags/create?access_token=" + accessToken;
            return await base.PostAsync<TagWrapper>(url, tagWrapper);
        }

        public async Task<ResponseModelBase> DeleteTagAsync(TagWrapper tagWrapper)
        {
            string accessToken = (await GetAccessTokenAsync()).AccessToken;
            string url = "/cgi-bin/tags/delete?access_token=" + accessToken;
            return await base.PostAsync<ResponseModelBase>(url, tagWrapper);
        }

        public async Task<TagsWrapper> GetTagsAsync()
        {
            string accessToken = (await GetAccessTokenAsync()).AccessToken;
            string url = "/cgi-bin/tags/get?access_token=" + accessToken;
            return await base.GetAsync<TagsWrapper>(url);
        }

        public async Task<TagIdList> GetUserTagsAsync(string openId)
        {
            string accessToken = (await GetAccessTokenAsync()).AccessToken;
            string url = "/cgi-bin/tags/getidlist?access_token=" + accessToken;
            return await base.PostAsync<TagIdList>(url, new { openid = openId });
        }

        public async Task<ResponseModelBase> UpdateTagAsync(TagWrapper tagWrapper)
        {
            string accessToken = (await GetAccessTokenAsync()).AccessToken;
            string url = "/cgi-bin/tags/update?access_token=" + accessToken;
            return await base.PostAsync<TagIdList>(url, tagWrapper);
        }
    }
}
