using System.Text.Json.Serialization;

namespace Alx.FacebookLogin.Data.Dtos
{
    public class ErrorDto
    {
        [JsonPropertyName("error")]
        public Error Error { get; set; }
    }

    public class Error
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
