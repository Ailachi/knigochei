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
                "INSERT INTO Author(FirstName, LastName, BirthDate, GenderId) " +
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

        public void Delete(int id)
        {
            int affectedRowsNum = Connection.Execute(
                "DELETE FROM Author " +
                "WHERE Id = @id",
                new { id },
                transaction: Transaction
            );
        }

        public void Delete(Author author)
        {
            throw new NotImplementedException();
        }

        public Author Find(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Author author)
        {
            throw new NotImplementedException();
        }
    }
}
