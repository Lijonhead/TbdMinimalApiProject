using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TbdMinimalMusicAPi.Models.Dtos
{
    public class SongDto
    {
        [JsonPropertyName("songId")]
        public int SongId { get; set; }
        [JsonPropertyName("songTitle")]
        public string SongTitle { get; set; }
    }
}
