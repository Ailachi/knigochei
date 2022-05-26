using Knigochei.Repository.AuthorRepo;
using Knigochei.Repository.BookRepo;
using Knigochei.Repository.CartRepo;
using Knigochei.Repository.GenreRepo;
using Knigochei.Repository.UserRepo;
using System.Data;
using System.Data.SqlClient;


namespace Knigochei.UnitOfWorkDapper
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IBookRepository _bookRepository;
        private IGenreRepository _genreRepository;
        private IUserRepository _userRepository;
        private IAuthorRepository _authorRepository;
        private ICartRepository _cartRepository;
        private bool _disposed;


        public UnitOfWork(string connectionString)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            _connection = sqlConnection;
            _connection.Open();
            _transaction = _connection.BeginTransaction();

        }

        public IBookRepository BookRepository { get => _bookRepository ?? (_bookRepository = new BookRepository(_transaction)); set { _bookRepository = value; } }
        public IGenreRepository GenreRepository { get => _genreRepository ?? (_genreRepository = new GenreRepository(_transaction)); set { _genreRepository = value; } }
        public IUserRepository UserRepository{ get => _userRepository ?? (_userRepository = new UserRepository(_transaction)); set { _userRepository = value; } }
        public IAuthorRepository AuthorRepository { get => _authorRepository ?? (_authorRepository = new AuthorRepository(_transaction)); set { _authorRepository = value; } }
        public ICartRepository CartRepository{ get => _cartRepository ?? (_cartRepository = new CartRepository(_transaction)); set { _cartRepository = value; } }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _bookRepository = null;
            _authorRepository = null;
            _genreRepository = null;
            _userRepository = null;
            _cartRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }

    }
}
