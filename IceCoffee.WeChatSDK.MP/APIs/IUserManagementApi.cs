using IceCoffee.WeChatSDK.MP.Models;
using IceCoffee.WeChatSDK.MP.Models.TagModels;
using System.Threading.Tasks;

namespace IceCoffee.WeChatSDK.MP.APIs
{
    public interface IUserManagementApi
    {
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
        Task<ResponseModelBase> BatchUnTaggingAsync(BatchTagging batchTagging);

        /// <summary>
        /// 创建标签
        /// </summary>
        /// <param name="tagEntry"></param>
        /// <returns></returns>
        Task<TagWrapper> CreateTagAsync(TagWrapper tagWrapper);

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="tagEntry"></param>
        /// <returns></returns>
        Task<ResponseModelBase> DeleteTagAsync(TagWrapper tagWrapper);

        /// <summary>
        /// 获取公众号已创建的标签
        /// </summary>
        /// <returns></returns>
        Task<TagsWrapper> GetTagsAsync();

        /// <summary>
        /// 获取用户身上的标签列表
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        Task<TagIdList> GetUserTagsAsync(string openId);

        /// <summary>
        /// 编辑标签
        /// </summary>
        /// <param name="tagEntry"></param>
        /// <returns></returns>
        Task<ResponseModelBase> UpdateTagAsync(TagWrapper tagWrapper);
    }
}