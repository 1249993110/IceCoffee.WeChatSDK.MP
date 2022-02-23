using IceCoffee.WeChatSDK.MP.Models.BasicApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCoffee.WeChatSDK.MP.APIs
{
    public interface IBasicApi
    {
        /// <summary>
        /// 通过 code 获取网页授权 access_token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<AccessTokenWrapper> GetAccessTokenAsync(string code);

        /// <summary>
        /// 获取全局唯一接口调用凭据
        /// </summary>
        /// <returns></returns>
        Task<AccessTokenWrapper> GetAccessTokenAsync();

        /// <summary>
        /// 获取 jsapi_ticket
        /// </summary>
        /// <returns></returns>
        Task<JsApiTicket> GetJsApiTicketAsync()
    }
}
