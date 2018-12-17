using DataAccess.Repositories;

namespace DataAccess.Context
{
    public sealed class UnitOfWork : System.IDisposable
    {
        // FIELDS
        private bool disposedValue = false; // To detect redundant calls
        private AppContext context;
        // PROPERTIES
        public UserRepository UserRepository
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public PhotoRepository PhotoRepository
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public PhotoLikeRepository PhotoLikeRepository
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public CommentRepository CommentRepository
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public CommentLikeRepository CommentLikeRepository
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public MessageRepository MessageRepository
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public SubjectRepository SubjectRepository
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        // CONSTRUCTORS
        public UnitOfWork(string connectionString)
        {
            throw new System.NotImplementedException();
        }

        // METHODS
        public void Save()
        {
            throw new System.NotImplementedException();
        }
        #region IDisposable Support
        protected void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
