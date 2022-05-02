using Knigochei.Models;
using System.Data;
using Dapper;

namespace Knigochei.Repository.GenreRepo
{
    internal class GenreRepository : RepositoryBase, IGenreRepository
    {
        public GenreRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }
        public void Add(Genre genre)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Genre> All()
        {
            return Connection.Query<Genre>(
                "SELECT * FROM Genre",
                transaction: Transaction
            ); ;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Genre entity)
        {
            throw new NotImplementedException();
        }

        public Book Find(int id)
        {
            throw new NotImplementedException();
        }

        public Book FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(Genre entity)
        {
            throw new NotImplementedException();
        }
    }
}
