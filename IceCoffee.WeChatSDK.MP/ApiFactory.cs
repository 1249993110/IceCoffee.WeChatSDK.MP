using IceCoffee.WeChatSDK.MP.Apis;
using IceCoffee.WeChatSDK.MP.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace IceCoffee.WeChatSDK.MP
{
    public class ApiFactory
    {
        private readonly Func<IServiceProvider, string, WeChatMpOpenApiOptions> _getOpenApiOptions;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpClientFactory _clientFactory;
        private readonly Dictionary<string, IApi> _cache;

        public ApiFactory(IOptions<ApiFactoryOptions> options, IServiceProvider serviceProvider)
        {
            this._getOpenApiOptions = options.Value.GetOpenApiOptions;
            this._serviceProvider = serviceProvider;
            this._memoryCache = serviceProvider.GetRequiredService<IMemoryCache>();
            this._clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            _cache = new Dictionary<string, IApi>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">optionsName 或 appId</param>
        /// <returns></returns>
        public T GetIApi<T>(string name) where T : IApi
        {
            Type type = typeof(T);
            string key = name + ":" + type.Name;

            if (_cache.TryGetValue(key, out var api) == false)
            {
                var options = _getOpenApiOptions.Invoke(_serviceProvider, name);
                api = (T)Activator.CreateInstance(type, new object[] { options, _memoryCache, _clientFactory });

                lock (_cache)
                {
                    _cache[key] = api;
                }
            }

            return (T)api;
        }
    }
}