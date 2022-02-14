using IceCoffee.WeChatSDK.MP.Models;
using IceCoffee.WeChatSDK.MP.Models.CustomMenuModels;

using IceCoffee.WeChatSDK.MP.Models.TagModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IceCoffee.WeChatSDK.MP.APIs
{
    public interface ICommonApi
    {
        /// <summary>
        /// 通过 code 获取网页授权 access_token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        AccessTokenModel GetAccessToken(string code);

        /// <summary>
        /// 通过 code 获取网页授权 access_token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<AccessTokenModel> GetAccessTokenAsync(string code);

        /// <summary>
        /// 获取全局唯一接口调用凭据
        /// </summary>
        /// <returns></returns>
        AccessTokenModel GetAccessToken();

        /// <summary>
        /// 获取全局唯一接口调用凭据
        /// </summary>
        /// <returns></returns>
        Task<AccessTokenModel> GetAccessTokenAsync();

        /// <summary>
        /// 获取 jsapi_ticket
        /// </summary>
        /// <returns></returns>
        JsApiTicketModel GetJsApiTicket();

        /// <summary>
        /// 获取 jsapi_ticket
        /// </summary>
        /// <returns></returns>
        Task<JsApiTicketModel> GetJsApiTicketAsync();

        /// <summary>
        /// 创建自定义菜单
        /// </summary>
        /// <returns></returns>
        ResponseModelBase CreateMenu(ButtonGroup buttonGroup);

        /// <summary>
        /// 创建自定义菜单
        /// </summary>
        /// <returns></returns>
        Task<ResponseModelBase> CreateMenuAsync(ButtonGroup buttonGroup);

        /// <summary>
        /// 创建标签
        /// </summary>
        /// <param name="tagEntry"></param>
        /// <returns></returns>
        Tag CreateTag(Tag tagEntry);

        /// <summary>
        /// 创建标签
        /// </summary>
        /// <param name="tagEntry"></param>
        /// <returns></returns>
        Task<Tag> CreateTagAsync(Tag tagEntry);

        /// <summary>
        /// 获取公众号已创建的标签
        /// </summary>
        /// <returns></returns>
        TagsWrapper GetTags();

        /// <summary>
        /// 获取公众号已创建的标签
        /// </summary>
        /// <returns></returns>
        Task<TagsWrapper> GetTagsAsync();

        /// <summary>
        /// 编辑标签
        /// </summary>
        /// <param name="tagEntry"></param>
        /// <returns></returns>
        ResponseModelBase UpdateTag(Tag tagEntry);

        /// <summary>
        /// 编辑标签
        /// </summary>
        /// <param name="tagEntry"></param>
        /// <returns></returns>
        Task<ResponseModelBase> UpdateTagAsync(Tag tagEntry);

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="tagEntry"></param>
        /// <returns></returns>
        ResponseModelBase DeleteTag(Tag tagEntry);

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="tagEntry"></param>
        /// <returns></returns>
        Task<ResponseModelBase> DeleteTagAsync(Tag tagEntry);

        /// <summary>
        /// 批量为用户打标签
        /// </summary>
        /// <remarks>
        /// 标签功能目前支持公众号为用户打上最多20个标签。
        /// </remarks>
        /// <param name="batchTagging"></param>
        /// <returns></returns>
        ResponseModelBase BatchTagging(BatchTagging batchTagging);

        /// <summary>
        /// 批量为用户打标签
        /// </summary>
        /// <remarks>
        /// 标签功能目前支持公众号为用户打上最多20个标签。
        /// </remarks>
        /// <param name="batchTagging"></param>
        /// <returns></returns>
        Task<ResponseModelBase> BatchTaggingAsync(BatchTagging batchTagging);

        /// <summary>
        /// 批量为用户取消标签
        /// </summary>
        /// <param name="batchTagging"></param>
        /// <returns></returns>
        ResponseModelBase BatchUnTagging(BatchTagging batchTagging);

        /// <summary>
        /// 批量为用户取消标签
        /// </summary>
        /// <param name="batchTagging"></param>
        /// <returns></returns>
        Task<ResponseModelBase> BatchUnTaggingAsync(BatchTagging batchTagging);

        /// <summary>
        /// 获取用户身上的标签列表
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        TagIdList GetUserTags(string openId);

        /// <summary>
        /// 获取用户身上的标签列表
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        Task<TagIdList> GetUserTagsAsync(string openId);
    }
}
