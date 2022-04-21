using Knigochei.Models;
using System.Data;
using Dapper;


namespace Knigochei.Repository.BookRepo
{
    internal class BookRepository : RepositoryBase, IBookRepository
    {
        public BookRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }

        public void Add(Book book)
        {
            book.Id = Connection.ExecuteScalar<int>(
                "INSERT INTO Book(Title, BookDescription, PublishedYear, Price, GenreId, AuthorId) " +
                "VALUES(@title, @descr, @year, @price, @genreId, @authorId); " +
                "SELECT SCOPE_IDENTITY()",
                param: new { @title=book.Title, @descr=book.BookDescription, @year=book.PublishedYear, @price=book.Price, 
                    @genreId=book.GenreId, @authorId=book.AuthorId },
                transaction: Transaction
            );
        }

        public IEnumerable<Book> All()
        {
            return Connection.Query<Book>(
                "SELECT * FROM Book",
                transaction: Transaction
            ).ToList();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Book entity)
        {
            throw new NotImplementedException();
        }

        public Book Find(int id)
        {
            throw new NotImplementedException();
        }

        public Book FindByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public void Update(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}
