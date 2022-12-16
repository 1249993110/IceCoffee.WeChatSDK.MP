using IceCoffee.Common.Extensions;
using IceCoffee.WeChatSDK.MP.Messages;
using IceCoffee.WeChatSDK.MP.Messages.CommonMessages;
using IceCoffee.WeChatSDK.MP.Messages.EventMessages;
using IceCoffee.WeChatSDK.MP.Options;
using IceCoffee.WeChatSDK.MP.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IceCoffee.WeChatSDK.MP
{
    /// <summary>
    /// Scoped lifetime
    /// </summary>
    public class MessageHandlerBase
    {
        private MessageBase _messageBase;

        private HttpContext _httpContext;
        private WeChatMpServerOptions _weChatMpOptions;
        private Func<MessageBase, string> _serializeDelegate;

        protected readonly ILogger<MessageHandlerBase> Logger;
        protected HttpContext HttpContext => _httpContext;
        public WeChatMpServerOptions WeChatMpOptions => _weChatMpOptions;

        public MessageHandlerBase(ILogger<MessageHandlerBase> logger)
        {
            this.Logger = logger;
        }

        public virtual async Task RunAsync(HttpContext httpContext, WeChatMpServerOptions weChatMpOptions)
        {
            _httpContext = httpContext;
            _weChatMpOptions = weChatMpOptions;

            try
            {
                #region 验证签名

                if (_httpContext.Request.Method == "GET")
                {
                    await OnGetAsync();
                    return;
                }

                #endregion 验证签名

                #region 接收微信消息

                await OnPostAsync();

                #endregion 接收微信消息
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "运行微信消息处理器出错");
                await _httpContext.Response.WriteAsync(ex.Message);
            }
        }

        /// <summary>
        /// 接收Get消息后
        /// </summary>
        /// <returns></returns>
        protected virtual async Task OnGetAsync()
        {
            var request = _httpContext.Request;
            var queryCollection = request.Query;
            var response = _httpContext.Response;

            // 验证签名
            if (Helper.CheckSignature(
                queryCollection["signature"],
                queryCollection["timestamp"],
                queryCollection["nonce"],
                _weChatMpOptions.Token))
            {
                await response.WriteAsync(queryCollection["echostr"]);
                return;
            }

            response.ContentType = "text/plain;charset=utf-8";
            response.StatusCode = StatusCodes.Status200OK;
            await response.WriteAsync("无效签名");
        }

        /// <summary>
        /// 接收Post消息后
        /// </summary>
        /// <returns></returns>
        protected virtual async Task OnPostAsync()
        {
            XDocument xDocument = await XDocument.LoadAsync(_httpContext.Request.Body, LoadOptions.None, CancellationToken.None);

            var rootElement = xDocument.Root;

            if (rootElement == null)
            {
                throw new Exception("rootElement 为空");
            }

            string msgType = rootElement.Element("MsgType").Value;

            MessageBase responseMessage = null;

            // 事件消息
            if (msgType == Constants.MsgType.Event)
            {
                string eventType = rootElement.Element("Event").Value;
                switch (eventType)
                {
                    case Constants.Event.Subscribe:
                        {
                            var requestMessage = MessageSerializer<FollowingUnfollowingMessage>.Deserialize(xDocument);
                            _messageBase = requestMessage;
                            responseMessage = await OnEvent_SubscribeRequestAsync(requestMessage);
                            break;
                        }
                    case Constants.Event.Unsubscribe:
                        {
                            var requestMessage = MessageSerializer<FollowingUnfollowingMessage>.Deserialize(xDocument);
                            _messageBase = requestMessage;
                            responseMessage = await OnEvent_UnsubscribeRequestAsync(requestMessage);
                            break;
                        }
                    default:
                        {
                            var requestMessage = MessageSerializer<MessageBase>.Deserialize(xDocument);
                            _messageBase = requestMessage;
                            responseMessage = await OnUnknownTypeRequestAsync(requestMessage);
                            break;
                        }
                }
            }
            else if (msgType == Constants.MsgType.Text)
            {
                var requestMessage = MessageSerializer<TextMessage>.Deserialize(xDocument);
                _messageBase = requestMessage;
                responseMessage = await OnTextRequestAsync(requestMessage);
            }
            else
            {
                responseMessage = await OnUnknownTypeRequestAsync(_messageBase);
            }

            await WriteResponse(responseMessage);
        }

        protected virtual async Task WriteSuccess()
        {
            _httpContext.Response.StatusCode = StatusCodes.Status200OK;
            _httpContext.Response.ContentType = "text/plain";
            await _httpContext.Response.WriteAsync("success");
        }

        protected virtual async Task WriteResponse(MessageBase responseMessage)
        {
            if (responseMessage == null)
            {
                await OnDefaultResponseAsync(_messageBase);
            }
            else
            {
                if (_serializeDelegate == null)
                {
                    throw new Exception("必须调用 CreateResponseMessage 创建响应消息");
                }

                try
                {
                    var response = _httpContext.Response;
                    response.StatusCode = StatusCodes.Status200OK;
                    response.ContentType = "text/xml";

                    string result = _serializeDelegate.Invoke(responseMessage);
                    await _httpContext.Response.WriteAsync(result);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _serializeDelegate = null;
                }
            }
        }

        protected virtual TMessage CreateResponseMessage<TMessage>() where TMessage : MessageBase, new()
        {
            _serializeDelegate = new Func<MessageBase, string>(MessageSerializer<TMessage>.Serialize);

            var message = new TMessage();
            message.ToUserName = _messageBase.FromUserName;
            message.FromUserName = _messageBase.ToUserName;
            message.CreateTime = DateTime.Now.ToTimeStamp();

            if (message is TextMessage textMessage)
            {
                textMessage.MsgType = Constants.MsgType.Text;
            }
            else if (message is EventMessage eventMessage)
            {
                eventMessage.MsgType = Constants.MsgType.Event;
            }

            return message;
        }

        /// <summary>
        /// 未知类型的消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual Task<MessageBase> OnUnknownTypeRequestAsync(MessageBase message)
        {
            return Task.FromResult<MessageBase>(null);
        }

        /// <summary>
        /// 默认消息，所有没有被处理的消息会默认返回此结果
        /// </summary>
        /// <param name="message">收到的消息</param>
        /// <returns></returns>
        protected virtual async Task OnDefaultResponseAsync(MessageBase message)
        {
            await WriteSuccess();
        }

        /// <summary>
        /// 关注事件
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual Task<MessageBase> OnEvent_SubscribeRequestAsync(FollowingUnfollowingMessage message)
        {
            return Task.FromResult<MessageBase>(null);
        }

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual Task<MessageBase> OnEvent_UnsubscribeRequestAsync(FollowingUnfollowingMessage message)
        {
            return Task.FromResult<MessageBase>(null);
        }

        protected virtual Task<MessageBase> OnTextRequestAsync(TextMessage message)
        {
            var responseMessage = CreateResponseMessage<TextMessage>();
            responseMessage.Content = "You send: " + message.Content;
            return Task.FromResult<MessageBase>(responseMessage);
        }
    }
}