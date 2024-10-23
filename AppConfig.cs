using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace QRCodeCreator
{
    [DataContract]
    public struct AppConfig
    {
        [DataMember]
        [JsonPropertyName("link")]
        public string Link { get; init; }
    }
}