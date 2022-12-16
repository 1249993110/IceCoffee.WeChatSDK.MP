using IceCoffee.WeChatSDK.MP.Models;
using IceCoffee.WeChatSDK.MP.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IceCoffee.WeChatSDK.MP.Apis
{
    public abstract class ApiBase : IApi
    {
        private readonly WeChatMpOpenApiOptions _options;
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpClientFactory _clientFactory;

        protected WeChatMpOpenApiOptions Options => _options;
        protected IMemoryCache MemoryCache => _memoryCache;

        public ApiBase(WeChatMpOpenApiOptions options, IMemoryCache memoryCache, IHttpClientFactory clientFactory)
        {
            _options = options;
            _memoryCache = memoryCache;
            _clientFactory = clientFactory;
        }

        public ApiBase(IOptions<WeChatMpOpenApiOptions> options, IMemoryCache memoryCache, IHttpClientFactory clientFactory)
            : this(options.Value, memoryCache, clientFactory)
        {
        }

        protected virtual HttpClient HttpClient => _clientFactory.CreateClient(Assembly.GetExecutingAssembly().FullName);

        /// <summary>
        /// 将object转换为查询参数附加到url后
        /// </summary>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual string AttachQueryString(string url, object obj)
        {
            try
            {
                var propertis = obj.GetType().GetProperties();
                var sb = new StringBuilder(url);

                sb.Append('?');
                foreach (var p in propertis)
                {
                    var v = p.GetValue(obj, null);
                    if (v == null)
                        continue;

                    sb.Append(p.Name);
                    sb.Append('=');
                    sb.Append(v.ToString());
                    sb.Append('&');
                }

                sb.Remove(sb.Length - 1, 1);

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ApiBase.AttachQueryString", ex);
            }
        }

        protected virtual string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions()
            {
#if NETCOREAPP3_1
                IgnoreNullValues = true,
#else
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
#endif
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping// 不进行 unicode编码，微信不支持/u....格式
            });
        }

        /// <summary>
        /// 使用 Get 返回异步请求直接返回对象
        /// </summary>
        /// <typeparam name="TResult">响应对象类型</typeparam>
        /// <param name="relativeUri">请求链接</param>
        /// <returns>返回请求的对象</returns>
        protected virtual async Task<TResult> GetAsync<TResult>(string relativeUri) where TResult : ResponseModelBase
        {
            try
            {
                using var httpResponse = await HttpClient.GetAsync(relativeUri);
                httpResponse.EnsureSuccessStatusCode();

                string responseBody = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResult>(responseBody);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ApiBase.GetAsync", ex);
            }
        }

        /// <summary>
        /// 使用 Get 返回异步请求直接返回对象
        /// </summary>
        /// <typeparam name="TResult">响应对象类型</typeparam>
        /// <param name="relativeUri">请求链接</param>
        /// <param name="args">使用反射自动转化为 QueryString</param>
        /// <returns>返回请求的对象</returns>
        protected virtual Task<TResult> GetAsync<TResult>(string relativeUri, object args) where TResult : ResponseModelBase
        {
            return GetAsync<TResult>(AttachQueryString(relativeUri, args));
        }

        /// <summary>
        /// 使用 Post 返回异步请求直接返回对象
        /// </summary>
        /// <typeparam name="TResult">响应对象类型</typeparam>
        /// <param name="url">请求链接</param>
        /// <param name="args">发送的对象,如果对象不是string类型则自动转换为Json对象</param>
        /// <returns>返回请求的对象</returns>
        protected virtual async Task<TResult> PostAsync<TResult>(string url, object args) where TResult : ResponseModelBase
        {
            try
            {
                string content = (args as string) ?? Serialize(args);

                using var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                using var httpResponse = await HttpClient.PostAsync(url, httpContent);
                httpResponse.EnsureSuccessStatusCode();

                string responseBody = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResult>(responseBody);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ApiBase.PostAsync", ex);
            }
        }

        /// <summary>
        /// 使用 Put 返回异步请求直接返回对象
        /// </summary>
        /// <typeparam name="TResult">响应对象类型</typeparam>
        /// <param name="url">请求链接</param>
        /// <param name="args">发送的对象,如果对象不是string类型则自动转换为Json对象</param>
        /// <returns>返回请求的对象</returns>
        protected virtual async Task<TResult> PutAsync<TResult>(string url, object args) where TResult : ResponseModelBase
        {
            try
            {
                string content = (args as string) ?? Serialize(args);

                using var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                using var httpResponse = await HttpClient.PutAsync(url, httpContent);
                httpResponse.EnsureSuccessStatusCode();

                string responseBody = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResult>(responseBody);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ApiBase.PutAsync", ex);
            }
        }

        /// <summary>
        /// 使用 Patch 返回异步请求直接返回对象
        /// </summary>
        /// <typeparam name="TResult">响应对象类型</typeparam>
        /// <param name="url">请求链接</param>
        /// <param name="args">发送的对象,如果对象不是string类型则自动转换为Json对象</param>
        /// <returns>返回请求的对象</returns>
        protected virtual async Task<TResult> PatchAsync<TResult>(string url, object args) where TResult : ResponseModelBase
        {
            try
            {
                string content = (args as string) ?? Serialize(args);

                using var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                using var httpResponse = await HttpClient.PatchAsync(url, httpContent);
                httpResponse.EnsureSuccessStatusCode();

                string responseBody = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResult>(responseBody);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ApiBase.PatchAsync", ex);
            }
        }

        /// <summary>
        /// 使用 Delete 返回异步请求直接返回对象
        /// </summary>
        /// <typeparam name="TResult">响应对象类型</typeparam>
        /// <param name="relativeUri">请求链接</param>
        /// <returns>返回请求的对象</returns>
        protected virtual async Task<TResult> DeleteAsync<TResult>(string relativeUri) where TResult : ResponseModelBase
        {
            try
            {
                using var httpResponse = await HttpClient.DeleteAsync(relativeUri);
                httpResponse.EnsureSuccessStatusCode();

                string responseBody = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResult>(responseBody);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ApiBase.DeleteAsync", ex);
            }
        }
    }
}