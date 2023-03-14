using IceCoffee.WeChatSDK.MP.Apis;
using IceCoffee.WeChatSDK.MP.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;

namespace IceCoffee.WeChatSDK.MP
{
    public class ApiFactory
    {
        private readonly IOptionsMonitor<WeChatMpOpenApiOptions> _optionsMonitor;
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpClientFactory _clientFactory;
        private readonly Dictionary<string, IApi> _cache;

        public ApiFactory(IOptionsMonitor<WeChatMpOpenApiOptions> optionsMonitor, IMemoryCache memoryCache, IHttpClientFactory httpClientFactory)
        {
            this._optionsMonitor = optionsMonitor;
            this._memoryCache = memoryCache;
            this._clientFactory = httpClientFactory;
            _cache = new Dictionary<string, IApi>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">optionsName</param>
        /// <returns></returns>
        public T GetIApi<T>(string name) where T : IApi
        {
            Type type = typeof(T);
            string key = name + ":" + type.Name;

            if (_cache.TryGetValue(key, out var api) == false)
            {
                var options = _optionsMonitor.Get(name);

                type = Type.GetType(string.Concat(type.Namespace, ".", type.Name.AsSpan(1)));
                api = (T)Activator.CreateInstance(type, new object[] { options, _memoryCache, _clientFactory });

                lock (_cache)
                {
                    _cache[key] = api;
                }
            }

            return (T)api;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public T GetIApi<T>(string name, WeChatMpOpenApiOptions options) where T : IApi
        {
            Type type = typeof(T);
            string key = name + ":" + type.Name;

            if (_cache.TryGetValue(key, out var api) == false)
            {
                type = Type.GetType(string.Concat(type.Namespace, ".", type.Name.AsSpan(1)));
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