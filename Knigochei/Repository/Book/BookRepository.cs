using System.Data;


namespace Knigochei.Repository.Book
{
    internal class BookRepository : RepositoryBase, IBookRepository
    {
        public BookRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }

    }
}
