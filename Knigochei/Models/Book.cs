namespace Knigochei.Models
{
    public class Book
    {
        public int Id { get; }
        public string Title { get; set; }
        public string BookDescription { get; set; }
        public int PublishedYear { get; set; }
        public float BookRank { get; set; }
        public int Price { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }

    }
}
