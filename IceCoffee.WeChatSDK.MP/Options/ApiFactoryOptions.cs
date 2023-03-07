using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IceCoffee.WeChatSDK.MP.Options
{
    public class ApiFactoryOptions
    {
        /// <summary>
        /// <para>第一个参数: <see cref="IServiceProvider"/></para>
        /// <para>第二个参数: <see cref="IServiceProvider"/></para>
        /// <para>第三个参数: <see cref="IServiceProvider"/></para>
        /// </summary>
        public Func<IServiceProvider, string, WeChatMpOpenApiOptions> GetOpenApiOptions { get; set; } = (serviceProvider, name) =>
        {
            return serviceProvider.GetRequiredService<IOptionsMonitor<WeChatMpOpenApiOptions>>().Get(name);
        };
    }
}