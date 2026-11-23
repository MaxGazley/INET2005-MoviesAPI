namespace MoviesAPI.DTOs
{
    public class CriticReview
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Review { get; set; } = string.Empty;
        public int Rating { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedDate { get; set; }
        public User User { get; set; } = null!;

    }
}
