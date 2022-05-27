using Knigochei.Models;
using Knigochei.Repository.BookRepo;
using Knigochei.UnitOfWorkDapper;

namespace Knigochei.Services.BookService
{
    public class BookService : IBookService
    {
        private IUnitOfWork _uow;

        public BookService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void AddBook(Book book)
        {
            IBookRepository repo = _uow.BookRepository;
            repo.Add(book);

            _uow.Commit();

        }

        public void DeleteBookById(int bookId)
        {
            IBookRepository repo = _uow.BookRepository;
            repo.Delete(bookId);

            _uow.Commit();
        }

        public List<Book> GetAllBooks()
        {
            IBookRepository repo = _uow.BookRepository;
            List<Book> books = repo.All().ToList();

            return books;
        }

        public Book GetBookById(int bookId)
        {
            IBookRepository repo = _uow.BookRepository;
            Book book = repo.Find(bookId);

            return book ?? new Book();
        }

        public void GetBookByTitle(string title)
        {
            throw new NotImplementedException();
        }
        public void FilterBooksByGenre(ref List<Book> books, int genreId)
        {
            if (genreId != 0)
                books = books.Where(book => book.GenreId == genreId).ToList();
        }

        public void FilterBooksByPrice(ref List<Book> books, int minPrice, int maxPrice)
        {
            books = books.Where(book => book.Price >= minPrice && book.Price <= maxPrice).ToList();
        }

        public void SortBooksByDesc(ref List<Book> books, bool isSortByDesc)
        {
            books = isSortByDesc ? books.OrderByDescending(book => book.Price).ToList() : books.OrderBy(book => book.Price).ToList();
        }
    }
}
