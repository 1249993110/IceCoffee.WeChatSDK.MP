using IceCoffee.WeChatSDK.MP.Messages;
using IceCoffee.WeChatSDK.MP.Messages.CommonMessages;
using IceCoffee.WeChatSDK.MP.Options;
using IceCoffee.WeChatSDK.MP.Serialization;
using IceCoffee.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IceCoffee.WeChatSDK.MP.Messages.EventMessages;
using System.Xml;
using System.Xml.Linq;
using System.Threading;
using System.Diagnostics;
using IceCoffee.WeChatSDK.MP.Messages.Primitives;

namespace IceCoffee.WeChatSDK.MP
{
    /// <summary>
    /// Scoped lifetime
    /// </summary>
    public class MessageHandlerBase
    {
        private MessageBase _messageBase;

        private HttpContext _httpContext;
        private WeChatMpOptions _weChatMpOptions;
        private Func<MessageBase, string> _serializeDelegate;

        protected readonly ILogger<MessageHandlerBase> logger;
        protected HttpContext HttpContext => _httpContext;
        public WeChatMpOptions WeChatMpOptions => _weChatMpOptions;

        public MessageHandlerBase(ILogger<MessageHandlerBase> logger)
        {
            this.logger = logger;
        }


        public virtual async Task RunAsync(HttpContext httpContext, WeChatMpOptions weChatMpOptions)
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

                #endregion

                #region 接收微信消息

                await OnPostAsync();

                #endregion
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "运行微信消息处理器出错");
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
            if (WeChatHelper.CheckSignature(
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
            
            if(rootElement == null)
            {
                throw new Exception("rootElement 为空");
            }

            string msgType = rootElement.Element("MsgType").Value;

            MessageBase responseMessage = null;
            
            // 事件消息
            if (msgType == ExportedConstants.MsgType.Event)
            {
                string eventType = rootElement.Element("Event").Value;
                switch (eventType)
                {
                    case ExportedConstants.Event.Subscribe:
                        {
                            var requestMessage = MessageSerializer<FollowingUnfollowingMessage>.Deserialize(xDocument);
                            _messageBase = requestMessage;
                            responseMessage = await OnEvent_SubscribeRequestAsync(requestMessage);
                            break;
                        }
                    case ExportedConstants.Event.Unsubscribe:
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
            else if (msgType == ExportedConstants.MsgType.Text)
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

                var response = _httpContext.Response;
                response.StatusCode = StatusCodes.Status200OK;
                response.ContentType = "text/xml";

                string result = _serializeDelegate.Invoke(responseMessage);
                _serializeDelegate = null;
                await _httpContext.Response.WriteAsync(result);
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
                textMessage.MsgType = ExportedConstants.MsgType.Text;
            }
            else if (message is EventMessage eventMessage)
            {
                eventMessage.MsgType = ExportedConstants.MsgType.Event;
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
