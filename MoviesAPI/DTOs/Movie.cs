using System.Text.Json.Serialization;

namespace MoviesAPI.DTOs
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int RunTime { get; set; }
        public string Synopsis { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public bool InTheaters { get; set; }
        public string ImageFileName { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public Genre Genre { get; set; } = null!;
        public AudienceRating AudienceRating { get; set; } = null!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<CriticReview> CriticReviews { get; set; } = null!;



    }
}
