using DataAccess.Repositories;

namespace DataAccess.Context
{
    /// <summary>
    /// Contains all the repositories
    /// <para/>
    /// Implements a Singleton pattern
    /// </summary>
    public sealed class UnitOfWork
    {
        // FIELDS
        private AppContext context;
        private static readonly UnitOfWork instance;

        private UserRepository userRepository;
        private PhotoRepository photoRepository;
        private PhotoLikeRepository photoLikeRepository;
        private CommentRepository commentRepository;
        private CommentLikeRepository commentLikeRepository;
        private MessageRepository messageRepository;
        private SubjectRepository subjectRepository;

        // CONSTRUCTORS
        private UnitOfWork(string connectionString)
        {
            context = new AppContext(connectionString);

            userRepository = null;
            photoRepository = null;
            photoLikeRepository = null;
            commentRepository = null;
            commentLikeRepository = null;
            messageRepository = null;
            subjectRepository = null;
        }
        private UnitOfWork()
        {
            context = new AppContext();

            userRepository = null;
            photoRepository = null;
            photoLikeRepository = null;
            commentRepository = null;
            commentLikeRepository = null;
            messageRepository = null;
            subjectRepository = null;
        }
        static UnitOfWork()
        {
            instance = new UnitOfWork();
        }
        /// <summary>
        /// Release managed resources
        /// </summary>
        ~UnitOfWork()
        {
            context.Dispose();
        }
        // PROPERTIES
        /// <summary>
        /// Gets an instance of <see cref="UnitOfWork"/>
        /// </summary>
        public static UnitOfWork Instance => instance;
        /// <summary>
        /// Gets user repository
        /// </summary>
        public UserRepository UserRepository
        {
            get
            {
                if (userRepository == null) userRepository = new UserRepository(context);
                return userRepository;
            }
        }
        /// <summary>
        /// Gets photo repository
        /// </summary>
        public PhotoRepository PhotoRepository
        {
            get
            {
                if (photoRepository == null) photoRepository = new PhotoRepository(context);
                return photoRepository;
            }
        }
        /// <summary>
        /// Gets photo like repository
        /// </summary>
        public PhotoLikeRepository PhotoLikeRepository
        {
            get
            {
                if (photoLikeRepository == null) photoLikeRepository = new PhotoLikeRepository(context);
                return photoLikeRepository;
            }
        }
        /// <summary>
        /// Gets comment repository
        /// </summary>
        public CommentRepository CommentRepository
        {
            get
            {
                if (commentRepository == null) commentRepository = new CommentRepository(context);
                return commentRepository;
            }
        }
        /// <summary>
        /// Gets comment like repository
        /// </summary>
        public CommentLikeRepository CommentLikeRepository
        {
            get
            {
                if (commentLikeRepository == null) commentLikeRepository = new CommentLikeRepository(context);
                return commentLikeRepository;
            }
        }
        /// <summary>
        /// Gets message repository
        /// </summary>
        public MessageRepository MessageRepository
        {
            get
            {
                if (messageRepository == null) messageRepository = new MessageRepository(context);
                return messageRepository;
            }
        }
        /// <summary>
        /// Gets subject repository
        /// </summary>
        public SubjectRepository SubjectRepository
        {
            get
            {
                if (subjectRepository == null) subjectRepository = new SubjectRepository(context);
                return subjectRepository;
            }
        }
       
        // METHODS
        /// <summary>
        /// Confirm all transaction to DataBase
        /// </summary>
        /// <returns>
        /// Amount of transaction that has been confirmed
        /// </returns>
        public int Save()
        {
            return context.SaveChanges();
        }
        /// <summary>
        /// Gets generic repository to current entity
        /// </summary>
        /// <typeparam name="TEntity">
        /// An entity type to which repository is required
        /// </typeparam>
        /// <returns>
        /// An instance of <see cref="GenericRepository{TEntity}"/>
        /// </returns>
        public GenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(context);
        }
        /// <summary>
        /// Gets Non-generic repository to current entity
        /// </summary>
        /// <param name="entityType">
        /// An entity type to which repository is required
        /// </param>
        /// <returns>
        /// An instance of <see cref="NonGenericRepository"/>
        /// </returns>
        public NonGenericRepository GetRepository(System.Type entityType)
        {            
            return new NonGenericRepository(context, entityType);
        }
    }
}
