using IceCoffee.WeChatSDK.MP.Middlewares;
using IceCoffee.WeChatSDK.MP.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using IceCoffee.WeChatSDK.MP.APIs;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Headers;

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
        public static IServiceCollection AddWeChatMessageHandler<TMessageHandler>(this IServiceCollection services,
            Action<WeChatMpOptions> configure, string optionsName = null) where TMessageHandler : MessageHandlerBase
        {
            return services.AddWeChatMessageHandler<TMessageHandler>(new ConfigureWeChatMpOptions(optionsName, typeof(TMessageHandler), configure));
        }

        /// <summary>
        /// 添加微信消息处理器
        /// </summary>
        /// <typeparam name="TMessageHandler"></typeparam>
        /// <param name="services"></param>
        /// <param name="config">sub-section</param>
        public static IServiceCollection AddWeChatMessageHandler<TMessageHandler>(this IServiceCollection services,
           IConfiguration config, string optionsName = null) where TMessageHandler : MessageHandlerBase
        {
            return services.AddWeChatMessageHandler<TMessageHandler>(new ConfigureWeChatMpOptions(optionsName, typeof(TMessageHandler), config));
        }

        private static IServiceCollection AddWeChatMessageHandler<TMessageHandler>(this IServiceCollection services,
           ConfigureWeChatMpOptions configureWeChatMpOptions) where TMessageHandler : MessageHandlerBase
        {
            try
            {
                if (configureWeChatMpOptions == null)
                {
                    throw new ArgumentNullException(nameof(configureWeChatMpOptions));
                }

                services.ConfigureOptions(configureWeChatMpOptions);
                // 内部已实现 TryAdd
                services.AddMemoryCache();
                services.TryAddScoped<TMessageHandler>();

                var assembly = typeof(ApiBase).Assembly;
                foreach (var item in assembly.GetExportedTypes().Where(t => t.IsSubclassOf(typeof(ApiBase))))
                {
                    var interfaceType = item.GetInterfaces().First();
                    services.TryAddSingleton(item);
                }

                services.AddHttpClient(ApiBase.HttpClientName, httpClient =>
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(2);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.BaseAddress = new Uri("https://api.weixin.qq.com/");
                });

                return services;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in IServiceCollectionExtension.AddWeChatMessageHandler", ex);
            }
        }
    }
}
