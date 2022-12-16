using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IceCoffee.WeChatSDK.MP.Options
{
    public class ConfigureWeChatMpServerOptions : ConfigureNamedOptions<WeChatMpServerOptions>
    {
        private readonly Type _messageHandlerType;

        public ConfigureWeChatMpServerOptions(string optionsName, Type messageHandlerType, Action<WeChatMpServerOptions> action)
            : base(optionsName, action)
        {
            _messageHandlerType = messageHandlerType;
        }

        public ConfigureWeChatMpServerOptions(string optionsName, Type messageHandlerType, IConfiguration config)
            : this(optionsName, messageHandlerType, config, null)
        {
        }

        public ConfigureWeChatMpServerOptions(string optionsName, Type messageHandlerType, IConfiguration config, Action<BinderOptions> configureBinder)
            : base(optionsName, options => config.Bind(options, configureBinder))
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            _messageHandlerType = messageHandlerType;
        }

        public override void Configure(string name, WeChatMpServerOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            // Null name is used to configure all named options.
            if (Name == null || name == Name)
            {
                options.messageHandlerType = _messageHandlerType;
                Action?.Invoke(options);
            }
        }
    }
}