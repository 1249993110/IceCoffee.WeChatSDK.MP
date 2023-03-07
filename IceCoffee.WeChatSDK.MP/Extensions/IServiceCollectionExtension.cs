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
        #region 添加微信公共号服务端消息处理器
        /// <summary>
        /// 添加微信公共号服务端消息处理器
        /// </summary>
        public static IServiceCollection AddWeChatMpServerMessageHandler<TMessageHandler>(
            this IServiceCollection services, Action<WeChatMpServerOptions> configure)
            where TMessageHandler : MessageHandlerBase
        {
            return services.InternalAddWeChatMpServerMessageHandler<TMessageHandler>(
                new ConfigureWeChatMpServerOptions(null, typeof(TMessageHandler), configure));
        }

        /// <summary>
        /// 添加微信公共号服务端消息处理器
        /// </summary>
        public static IServiceCollection AddWeChatMpServerMessageHandler<TMessageHandler>(this IServiceCollection services,
            string name, Action<WeChatMpServerOptions> configure)
            where TMessageHandler : MessageHandlerBase
        {
            return services.InternalAddWeChatMpServerMessageHandler<TMessageHandler>(
                new ConfigureWeChatMpServerOptions(name, typeof(TMessageHandler), configure));
        }

        /// <summary>
        /// 添加微信公共号服务端消息处理器
        /// </summary>
        public static IServiceCollection AddWeChatMpServerMessageHandler<TMessageHandler>(this IServiceCollection services,
           IConfigurationSection configSection)
            where TMessageHandler : MessageHandlerBase
        {
            return services.InternalAddWeChatMpServerMessageHandler<TMessageHandler>(
                new ConfigureWeChatMpServerOptions(null, typeof(TMessageHandler), configSection));
        }

        /// <summary>
        /// 添加微信公共号服务端消息处理器
        /// </summary>
        public static IServiceCollection AddWeChatMpServerMessageHandler<TMessageHandler>(this IServiceCollection services,
           string name, IConfigurationSection configSection)
           where TMessageHandler : MessageHandlerBase
        {
            return services.InternalAddWeChatMpServerMessageHandler<TMessageHandler>(
                new ConfigureWeChatMpServerOptions(name, typeof(TMessageHandler), configSection));
        }

        private static IServiceCollection InternalAddWeChatMpServerMessageHandler<TMessageHandler>(this IServiceCollection services,
           ConfigureWeChatMpServerOptions configureWeChatMpOptions) where TMessageHandler : MessageHandlerBase
        {
            try
            {
                if (configureWeChatMpOptions == null)
                {
                    throw new ArgumentNullException(nameof(configureWeChatMpOptions));
                }

                services.ConfigureOptions(configureWeChatMpOptions);
                services.TryAddScoped<TMessageHandler>();
                return services;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in IServiceCollectionExtension.AddWeChatMpServerMessageHandler", ex);
            }
        }

        #endregion

        public static IServiceCollection AddWeChatMpOpenApi(this IServiceCollection services, Action<WeChatMpOpenApiOptions> configure)
        {
            services.Configure(configure);
            services.InternalAddWeChatMpOpenApi();
            return services;
        }

        public static IServiceCollection AddWeChatMpOpenApi(this IServiceCollection services, string name, Action<WeChatMpOpenApiOptions> configure)
        {
            services.Configure(name, configure);
            services.InternalAddWeChatMpOpenApi();
            return services;
        }

        public static IServiceCollection AddWeChatMpOpenApi(this IServiceCollection services, IConfigurationSection configSection)
        {
            services.Configure<WeChatMpOpenApiOptions>(configSection);
            services.InternalAddWeChatMpOpenApi();
            return services;
        }

        public static IServiceCollection AddWeChatMpOpenApi(this IServiceCollection services, string name, IConfigurationSection configSection)
        {
            services.Configure<WeChatMpOpenApiOptions>(name, configSection);
            services.InternalAddWeChatMpOpenApi();
            return services;
        }

        private static IServiceCollection InternalAddWeChatMpOpenApi(this IServiceCollection services)
        {
            services.AddMemoryCache();

            var assembly = Assembly.GetExecutingAssembly();
            foreach (var item in assembly.GetExportedTypes().Where(t => t.IsSubclassOf(typeof(ApiBase))))
            {
                var interfaceType = item.GetInterfaces().First(t => t != typeof(IApi));
                services.TryAddSingleton(interfaceType, item);
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

        public static IServiceCollection AddApiFactory(this IServiceCollection services)
        {
            services.TryAddSingleton<ApiFactory>();
            return services;
        }

        public static IServiceCollection AddApiFactory(this IServiceCollection services, Action<ApiFactoryOptions> configure)
        {
            services.Configure(configure);
            services.TryAddSingleton<ApiFactory>();
            return services;
        }
    }
}
