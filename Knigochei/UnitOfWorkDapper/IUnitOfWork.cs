using Knigochei.Repository.BookRepo;



namespace Knigochei.UnitOfWorkDapper
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; set; }
        void Commit();
    }
}
