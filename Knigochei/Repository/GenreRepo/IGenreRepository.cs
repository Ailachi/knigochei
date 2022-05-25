using Knigochei.Models;

namespace Knigochei.Repository.GenreRepo
{
    public interface IGenreRepository
    {
        void Add(Genre genre);
        IEnumerable<Genre> All();
        void Delete(int id);
        void Delete(Genre entity);
        Book Find(int id);
        Book FindByName(string name);
        void Update(Genre entity);
    }
}
