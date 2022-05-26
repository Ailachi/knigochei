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
            throw new NotImplementedException();
        }

        public void DeleteBookById(int bookId)
        {
            throw new NotImplementedException();
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
    }
}
