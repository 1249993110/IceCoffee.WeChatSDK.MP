using IceCoffee.WeChatSDK.MP.Messages;
using System;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace IceCoffee.WeChatSDK.MP.Serialization
{
    /// <summary>
    /// MessageSerializer
    /// </summary>
    public static class MessageSerializer<TMessage> where TMessage : MessageBase
    {
        private static readonly XmlSerializer _xmlSerializer;

        static MessageSerializer()
        {
            _xmlSerializer = new XmlSerializer(typeof(TMessage), new XmlRootAttribute("xml"));
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(MessageBase obj)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = false;
                settings.Encoding = Encoding.UTF8;
                settings.OmitXmlDeclaration = true;
                settings.Async = true;

                XmlWriter writer = XmlWriter.Create(stringBuilder, settings);

                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);// 去除 xmlns:xsd

                _xmlSerializer.Serialize(writer, obj, ns);

                return stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("序列化失败", ex);
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="xDocument"></param>
        /// <returns></returns>
        public static TMessage Deserialize(XDocument xDocument)
        {
            try
            {
                XmlReader xmlReader = xDocument.CreateReader();

                return _xmlSerializer.Deserialize(xmlReader) as TMessage;
            }
            catch (Exception ex)
            {
                throw new Exception("反序列化失败", ex);
            }
        }
    }
}