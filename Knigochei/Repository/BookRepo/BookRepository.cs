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
                sql: "INSERT INTO Book(Title, BookDescription, PublishedYear, Price, CoverImagePath, GenreId, AuthorId) " +
                "VALUES(@title, @descr, @year, @price, @imagePath, @genreId, @authorId); " +
                "SELECT SCOPE_IDENTITY()",
                param: 
                new 
                {
                    @title = book.Title,
                    @descr = book.BookDescription,
                    @year = book.PublishedYear,
                    @price = book.Price,
                    @imagePath = book.CoverImagePath,
                    @genreId = book.GenreId,
                    @authorId = book.AuthorId
                },
                transaction: Transaction
            );
        }

        public IEnumerable<Book> All()
        {
            return Connection.Query<Book>(
                sql: "SELECT * FROM Book WHERE IsDeleted = 0",
                transaction: Transaction
            );
        }

        public void Delete(int id)
        {
            int affectedRowsNum = Connection.Execute(
                sql: "UPDATE Book " +
                     "SET IsDeleted = 1 " +
                     "WHERE Id = @id",
                param: new { id },
                transaction: Transaction
            );

        }

        public void Delete(Book entity)
        {
            throw new NotImplementedException();
        }

        public Book Find(int bookId)
        {
            return Connection.Query<Book>(
                sql: "SELECT * from Book WHERE Id = @id AND IsDeleted = 0",
                param: new { @id = bookId },
                transaction: Transaction
            ).FirstOrDefault();
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
