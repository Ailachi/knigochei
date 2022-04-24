using Knigochei.Models;

namespace Knigochei.Services.BookService
{
    public interface IBookService
    {
        public List<Book> GetAllBooks();
        public void AddBook(Book book);
        public void DeleteBookById(int bookId);
        public void GetBookById(int bookId);
        public void GetBookByTitle(string title);
    }
}
