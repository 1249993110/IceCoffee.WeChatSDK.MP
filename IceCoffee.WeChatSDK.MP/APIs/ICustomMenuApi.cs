using IceCoffee.WeChatSDK.MP.Models;
using IceCoffee.WeChatSDK.MP.Models.CustomMenuModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCoffee.WeChatSDK.MP.Apis
{
    public interface ICustomMenuApi : IApi
    {
        /// <summary>
        /// 创建自定义菜单
        /// </summary>
        /// <returns></returns>
        Task<ResponseModelBase> CreateMenuAsync(ButtonGroup buttonGroup);
    }
}
