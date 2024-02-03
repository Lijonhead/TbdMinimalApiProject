using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TbdMinimalMusicAPi.Models.Dtos
{
    public class UserDto
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
    }
}
