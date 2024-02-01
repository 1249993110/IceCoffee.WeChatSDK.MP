using IceCoffee.WeChatSDK.MP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCoffee.WeChatSDK.MP.Apis
{
    public interface ITemplateMessageApi
    {
        /// <summary>
        /// 发送模板消息
        /// </summary>
        Task<ResponseModelBase> SendAsync(TemplateMessage templateMessage);
    }
}
