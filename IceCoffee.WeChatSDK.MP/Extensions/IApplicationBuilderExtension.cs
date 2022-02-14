using IceCoffee.WeChatSDK.MP.Middlewares;
using IceCoffee.WeChatSDK.MP.Options;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace IceCoffee.WeChatSDK.MP.Extensions
{
    /// <summary>
    /// 微信中间件扩展
    /// </summary>
    public static class IApplicationBuilderExtension
    {
        public static IApplicationBuilder UseWeChatMP(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<WeChatMiddleware>();
        }

        public static IApplicationBuilder UseWeChatMP(this IApplicationBuilder app, string optionsName)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (optionsName == null)
            {
                throw new ArgumentNullException(nameof(optionsName));
            }

            return app.UseMiddleware<WeChatMiddleware>(optionsName);
        }

        public static IApplicationBuilder UseWeChatMP(this IApplicationBuilder app, WeChatMpOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return app.UseMiddleware<WeChatMiddleware>(Microsoft.Extensions.Options.Options.Create(options));
        }
    }
}
