using System.Text.Json.Serialization;

namespace TbdMinimalMusicAPi.Models.Dtos
{
    public class GenreDto
    {
        [JsonPropertyName("genreId")]
        public int GenreId { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}
