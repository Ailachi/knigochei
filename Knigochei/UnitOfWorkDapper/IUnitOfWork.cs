using Knigochei.Repository.Book;

namespace Knigochei.UnitOfWorkDapper
{
    public interface IUnitOfWork : IDisposable
    {
        
        IBookRepository BookRepository { get; set; }
        void Commit();

    }
}
