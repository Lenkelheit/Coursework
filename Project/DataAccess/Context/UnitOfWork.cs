using DataAccess.Repositories;

namespace DataAccess.Context
{
    public sealed class UnitOfWork
    {
        // FIELDS
        private AppContext context;
        private static UnitOfWork instance;

        // CONSTRUCTORS
        public UnitOfWork(string connectionString)
        {
            throw new System.NotImplementedException();
        }
        static UnitOfWork()
        {
            throw new System.NotImplementedException();
        }
        ~UnitOfWork()
        {
            throw new System.NotImplementedException();
        }
        // PROPERTIES
        public static UnitOfWork Instance
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
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
       

        // METHODS
        public void Save()
        {
            throw new System.NotImplementedException();
        }
        
    }
}
