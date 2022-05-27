using Knigochei.Models;

namespace Knigochei.Services.BookService
{
    public interface IBookService
    {
        public List<Book> GetAllBooks();
        public void AddBook(Book book);
        public void DeleteBookById(int bookId);
        public Book GetBookById(int bookId);
        public void GetBookByTitle(string title);
        public void FilterBooksByGenre(ref List<Book> books, int genreId);
        public void FilterBooksByPrice(ref List<Book> books, int minPrice, int maxPrice);
        public void SortBooksByDesc(ref List<Book> books, bool isSortByDesc);
    }
}
