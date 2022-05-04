using Knigochei.Repository.BookRepo;
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
        private bool _disposed;


        public UnitOfWork(string connectionString)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            _connection = sqlConnection;
            _connection.Open();
            _transaction = _connection.BeginTransaction();

        }

        public IBookRepository BookRepository { get => _bookRepository ?? (_bookRepository = new BookRepository(_transaction)); }
        public IGenreRepository GenreRepository { get => _genreRepository ?? (_genreRepository = new GenreRepository(_transaction)); }
        public IUserRepository UserRepository{ get => _userRepository ?? (_userRepository = new UserRepository(_transaction)); }

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
