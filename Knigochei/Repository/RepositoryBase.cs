using System.Data;



namespace Knigochei.Repository
{
    internal abstract class RepositoryBase
    {
        protected IDbConnection Connection { get { return Transaction.Connection} }
        protected IDbTransaction Transaction { get; private set; }

        public RepositoryBase (IDbTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}
