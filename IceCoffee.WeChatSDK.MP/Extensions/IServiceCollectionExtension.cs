using IceCoffee.WeChatSDK.MP.Middlewares;
using IceCoffee.WeChatSDK.MP.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using IceCoffee.WeChatSDK.MP.Apis;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Headers;
using System.Reflection;

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
        /// <param name="optionsName"></param>
        /// <param name="autoRegisterApi"></param>
        public static IServiceCollection AddWeChatMessageHandler<TMessageHandler>(this IServiceCollection services,
            Action<WeChatMpOptions> configure, string optionsName = null, bool autoRegisterApi = true) where TMessageHandler : MessageHandlerBase
        {
            return services.AddWeChatMessageHandler<TMessageHandler>(
                new ConfigureWeChatMpOptions(optionsName, typeof(TMessageHandler), configure), autoRegisterApi);
        }

        /// <summary>
        /// 添加微信消息处理器
        /// </summary>
        /// <typeparam name="TMessageHandler"></typeparam>
        /// <param name="services"></param>
        /// <param name="configSection">sub-section</param>
        /// <param name="optionsName"></param>
        /// <param name="autoRegisterApi"></param>
        public static IServiceCollection AddWeChatMessageHandler<TMessageHandler>(this IServiceCollection services,
           IConfigurationSection configSection, string optionsName = null, bool autoRegisterApi = true) where TMessageHandler : MessageHandlerBase
        {
            return services.AddWeChatMessageHandler<TMessageHandler>(
                new ConfigureWeChatMpOptions(optionsName, typeof(TMessageHandler), configSection), autoRegisterApi);
        }

        private static IServiceCollection AddWeChatMessageHandler<TMessageHandler>(this IServiceCollection services,
           ConfigureWeChatMpOptions configureWeChatMpOptions, bool autoRegisterApi) where TMessageHandler : MessageHandlerBase
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

                var assembly = Assembly.GetExecutingAssembly();

                if (autoRegisterApi)
                {
                    foreach (var item in assembly.GetExportedTypes().Where(t => t.IsSubclassOf(typeof(ApiBase))))
                    {
                        var interfaceType = item.GetInterfaces().First(t => t != typeof(IApi));
                        services.TryAddSingleton(interfaceType, item);
                    }
                }

                services.AddHttpClient(assembly.FullName, httpClient =>
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(10);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
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
