using System.Text.Json.Serialization;

namespace TbdMinimalMusicAPi.Models.Dtos
{
    public class ArtistDto
    {
        [JsonPropertyName("artistId")]
        public int ArtistId { get; set; }
        [JsonPropertyName("artistName")]
        public string? ArtistName { get; set; }
        [JsonPropertyName("artistDescription")]
        public string ArtistDescription { get; set; }
    }
}
