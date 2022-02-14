using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace IceCoffee.WeChatSDK.MP.Options
{
    public class ConfigureWeChatMpOptions : ConfigureNamedOptions<WeChatMpOptions>
    {
        private readonly Type _messageHandlerType;

        public ConfigureWeChatMpOptions(string optionsName, Type messageHandlerType, Action<WeChatMpOptions> action) 
            : base(optionsName, action)
        {
            _messageHandlerType = messageHandlerType;
        }

        public ConfigureWeChatMpOptions(string optionsName, Type messageHandlerType, IConfiguration config)
            : this(optionsName, messageHandlerType, config, null)
        {
        }

        public ConfigureWeChatMpOptions(string optionsName, Type messageHandlerType, IConfiguration config, Action<BinderOptions> configureBinder) 
            : base(optionsName, options => config.Bind(options, configureBinder))
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            _messageHandlerType = messageHandlerType;
        }

        public override void Configure(string name, WeChatMpOptions options)
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
