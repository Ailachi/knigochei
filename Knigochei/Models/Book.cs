namespace Knigochei.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BookDescription { get; set; }
        public int PublishedYear { get; set; }
        public int Price { get; set; }
        public string CoverImagePath { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public float BookRank { get; set; }

        public Book() { }

        public Book(string title, string bookDescription, int publishedYear, int price, int genreId, int authorId, float bookRank)
        {
            this.Title = title;
            this.BookDescription = bookDescription;
            this.PublishedYear = publishedYear;
            this.Price = price;
            this.GenreId = genreId;
            this.AuthorId = authorId;
            this.BookRank = bookRank;
        }

    }
}
