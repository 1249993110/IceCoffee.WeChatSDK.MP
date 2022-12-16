using IceCoffee.WeChatSDK.MP.Apis;
using IceCoffee.WeChatSDK.MP.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace IceCoffee.WeChatSDK.MP
{
    public class ApiFactory
    {
        private readonly IOptionsMonitor<WeChatMpOpenApiOptions> _optionsMonitor;
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpClientFactory _clientFactory;
        private readonly Dictionary<string, IApi> _cache;

        public ApiFactory(IOptionsMonitor<WeChatMpOpenApiOptions> optionsMonitor, IMemoryCache memoryCache, IHttpClientFactory clientFactory)
        {
            this._optionsMonitor = optionsMonitor;
            this._memoryCache = memoryCache;
            this._clientFactory = clientFactory;
            _cache = new Dictionary<string, IApi>();
        }

        public T GetIApi<T>(string optionsName) where T : IApi
        {
            Type type = typeof(T);
            string key = optionsName + ":" + type.Name;

            if (_cache.TryGetValue(key, out var api) == false)
            {
                var options = _optionsMonitor.Get(optionsName);
                api = (T)Activator.CreateInstance(type, new object[] { options, _memoryCache, _clientFactory });

                _cache.Add(key, api);
            }

            return (T)api;
        }
    }
}