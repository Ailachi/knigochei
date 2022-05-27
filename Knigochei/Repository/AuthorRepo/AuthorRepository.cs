using Dapper;
using Knigochei.Models;
using System.Data;

namespace Knigochei.Repository.AuthorRepo
{
    internal class AuthorRepository : RepositoryBase, IAuthorRepository
    {
        public AuthorRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public void Add(Author author)
        {
            author.Id = Connection.ExecuteScalar<int>(
                sql: "INSERT INTO Author(FirstName, LastName, BirthDate, GenderId) " +
                "VALUES(@firstName, @lastName, @birthDate, @genderId); " +
                "SELECT SCOPE_IDENTITY()",
                param: new
                {
                    @firstName = author.FirstName,
                    @lastName = author.LastName,
                    @birthDate = author.BirthDate,
                    @genderId = author.GenderId
                },
                transaction: Transaction
            );
        }

        public IEnumerable<Author> All()
        {
            return Connection.Query<Author>(
                "SELECT * FROM Author",
                transaction: Transaction
            );
        }

        public IEnumerable<Author> GetAuthorsByGenreId(int genreId)
        {
            return Connection.Query<Author>(
                sql: "SELECT * FROM Author a " +
                     "INNER JOIN Book b on b.AuthorId = a.Id " +
                     "WHERE b.GenreId = @id ORDER BY a.FirstName",
                param: new { @id = genreId },
                transaction: Transaction
            );
        }

        public void Delete(int id)
        {
            int affectedRowsNum = Connection.Execute(
                sql: "DELETE FROM Author " +
                     "WHERE Id = @id",
                param: new { id },
                transaction: Transaction
            );
        }

        public void Delete(Author author)
        {
            throw new NotImplementedException();
        }

        public Author Find(int id)
        {
            return Connection.Query<Author>(
                sql: "SELECT * FROM Author WHERE Id = @id",
                param: new { @id },
                transaction: Transaction
            ).FirstOrDefault();
        }
        public Author FindAuthorByBookId(int bookId)
        {
            return Connection.Query<Author>(
                sql: "SELECT * FROM Author a " +
                     "INNER JOIN Book b ON b.AuthorId = a.Id " +
                     "WHERE b.Id = @id",
                param: new { @id = bookId },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Update(Author author)
        {
            throw new NotImplementedException();
        }

        
    }
}
