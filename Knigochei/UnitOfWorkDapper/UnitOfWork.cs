using Knigochei.Repository.Book;
using Dapper;
using System.Data;
using System.Data.SqlClient;


namespace Knigochei.UnitOfWorkDapper
{
    public class UnitOfWork: IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IBookRepository _bookRepository;
        private bool _disposed;

        public IBookRepository BookRepository { get => _bookRepository ?? (_bookRepository = new BookRepository(_transaction); }
        

        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }


        private void resetRepositories()
        {
            _bookRepository = null;
        }


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
