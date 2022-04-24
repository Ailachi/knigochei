using Knigochei.Models;



namespace Knigochei.Repository.BookRepo
{
    public interface IBookRepository
    {
        void Add(Book book);
        IEnumerable<Book> All();
        void Delete(int id);
        void Delete(Book entity);
        Book Find(int id);
        Book FindByTitle(string title);
        void Update(Book entity);

    }
}
