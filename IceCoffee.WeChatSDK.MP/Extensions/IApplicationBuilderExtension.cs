using IceCoffee.WeChatSDK.MP.Middlewares;
using IceCoffee.WeChatSDK.MP.Options;
using Microsoft.AspNetCore.Builder;

namespace IceCoffee.WeChatSDK.MP.Extensions
{
    /// <summary>
    /// 微信中间件扩展
    /// </summary>
    public static class IApplicationBuilderExtension
    {
        public static IApplicationBuilder UseWeChatMpServer(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<WeChatMpServerMiddleware>();
        }

        public static IApplicationBuilder UseWeChatMpServer(this IApplicationBuilder app, string optionsName)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (optionsName == null)
            {
                throw new ArgumentNullException(nameof(optionsName));
            }

            return app.UseMiddleware<WeChatMpServerMiddleware>(optionsName);
        }

        public static IApplicationBuilder UseWeChatMpServer(this IApplicationBuilder app, WeChatMpServerOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return app.UseMiddleware<WeChatMpServerMiddleware>(Microsoft.Extensions.Options.Options.Create(options));
        }
    }
}