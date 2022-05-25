using Knigochei.Models;
using Knigochei.Repository.GenreRepo;
using Knigochei.UnitOfWorkDapper;

namespace Knigochei.Services.GenreService
{
    public class GenreService : IGenreService
    {

        private IUnitOfWork _uow;

        public GenreService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public List<Genre> GetAllGenres()
        {
            IGenreRepository repo = _uow.GenreRepository;
            List<Genre> allGenres = repo.All().ToList();
            return allGenres;

        }

    }
}
