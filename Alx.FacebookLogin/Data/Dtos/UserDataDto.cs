using System.Text.Json.Serialization;

namespace Alx.FacebookLogin.Data.Dtos
{
    public class UserDataDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("picture")]
        public Picture Picture { get; set; }
    }

    public class Picture
    {
        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
