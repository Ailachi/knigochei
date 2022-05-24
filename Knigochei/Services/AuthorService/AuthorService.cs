using Knigochei.Models;
using Knigochei.Repository.AuthorRepo;
using Knigochei.UnitOfWorkDapper;

namespace Knigochei.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private IUnitOfWork _uow;

        public AuthorService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public void AddAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public void DeleteAuthorById(int authorId)
        {
            throw new NotImplementedException();
        }

        public List<Author> GetAllAuthors()
        {
            IAuthorRepository repo = _uow.AuthorRepository;
            List<Author> authors = repo.All().ToList();

            return authors;
        }

        public void GetAuthorById(int authorId)
        {
            throw new NotImplementedException();
        }
    }
}
