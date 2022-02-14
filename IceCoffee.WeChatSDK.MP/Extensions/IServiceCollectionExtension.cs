using IceCoffee.WeChatSDK.MP.Middlewares;
using IceCoffee.WeChatSDK.MP.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using IceCoffee.WeChatSDK.MP.APIs;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IceCoffee.WeChatSDK.MP.Extensions
{
    /// <summary>
    /// 微信服务扩展类
    /// </summary>
    public static class IServiceCollectionExtension
    {
        /// <summary>
        /// 添加微信消息处理器
        /// </summary>
        /// <typeparam name="TMessageHandler"></typeparam>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        public static void AddWeChatMessageHandler<TMessageHandler>(this IServiceCollection services,
            Action<WeChatMpOptions> configure, string optionsName = null) where TMessageHandler : MessageHandlerBase
        {
            services.ConfigureOptions(new ConfigureWeChatMpOptions(optionsName, typeof(TMessageHandler), configure));
            // 内部已实现 TryAdd
            services.AddMemoryCache();
            services.TryAddScoped<TMessageHandler>();
            services.TryAddSingleton<ICommonApi, CommonApi>();
        }

        /// <summary>
        /// 添加微信消息处理器
        /// </summary>
        /// <typeparam name="TMessageHandler"></typeparam>
        /// <param name="services"></param>
        /// <param name="config">sub-section</param>
        public static void AddWeChatMessageHandler<TMessageHandler>(this IServiceCollection services,
           IConfiguration config, string optionsName = null) where TMessageHandler : MessageHandlerBase
        {
            services.ConfigureOptions(new ConfigureWeChatMpOptions(optionsName, typeof(TMessageHandler), config));
            // 内部已实现 TryAdd
            services.AddMemoryCache();
            services.TryAddScoped<TMessageHandler>();
            services.TryAddSingleton<ICommonApi, CommonApi>();
        }
    }
}
