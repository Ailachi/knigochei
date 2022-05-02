using Knigochei.Repository.BookRepo;
using Knigochei.Repository.GenreRepo;

namespace Knigochei.UnitOfWorkDapper
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; set; }
        IGenreRepository GenreRepository{ get; set; }
        void Commit();
    }
}
