using IceCoffee.WeChatSDK.MP.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace IceCoffee.WeChatSDK.MP.Middlewares
{
    /// <summary>
    /// 微信公众号服务端中间件
    /// </summary>
    public class WeChatMpServerMiddleware
    {
        /// <summary>
        /// 要执行的下一个中间件，singleton-lifetime
        /// </summary>
        private readonly RequestDelegate _next;

        private readonly string _optionsName;
        private WeChatMpServerOptions _options;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="next"></param>
        public WeChatMpServerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="next"></param>
        /// <param name="optionsName"></param>
        public WeChatMpServerMiddleware(RequestDelegate next, string optionsName)
        {
            _next = next;
            _optionsName = optionsName;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        public WeChatMpServerMiddleware(RequestDelegate next, IOptions<WeChatMpServerOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            if (_options == null)
            {
                if (_optionsName == null)
                {
                    _options = context.RequestServices.GetRequiredService<IOptions<WeChatMpServerOptions>>().Value;
                }
                else
                {
                    _options = context.RequestServices.GetRequiredService<IOptionsMonitor<WeChatMpServerOptions>>().Get(_optionsName);
                }
            }

            if (context.Request.Path == _options.NotifyPath)
            {
                var messageHandler = context.RequestServices.GetRequiredService(_options.messageHandlerType) as MessageHandlerBase;
                await messageHandler.RunAsync(context, _options);
                return;
            }

            await _next(context);
        }
    }
}