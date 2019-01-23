namespace Galagram.ViewModel.Commands.User.PhotoInside
{
    /// <summary>
    /// Sets like or dislike to current comment
    /// </summary>
    public class LikeCommentCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.PhotoInsideViewModel photoInsideViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="LikeCommentCommand"/>
        /// </summary>
        /// <param name="photoInsideViewModel">
        /// An instance of <see cref="ViewModel.User.PhotoInsideViewModel"/>
        /// </param>
        public LikeCommentCommand(ViewModel.User.PhotoInsideViewModel photoInsideViewModel)
        {
            this.photoInsideViewModel = photoInsideViewModel;
        }

        // METHODS
        /// <summary>
        /// Check if command can be executed
        /// </summary>
        /// <param name="parameter">
        /// Additionals parameters
        /// </param>
        /// <returns>
        /// True if command can be executed, otherwise — false
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(LikeCommentCommand)}");
            return true;
        }
        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="parameter">
        /// Current comment
        /// <para/>
        /// An array of values :
        /// <para/>
        /// 1 — bool value. True if like, false if dislike <para/>
        /// 2 — current comments
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(LikeCommentCommand)}");

            // gets value
            bool isLike = System.Convert.ToBoolean((parameter as System.Array).GetValue(0));
            DataAccess.Entities.Comment comment = (DataAccess.Entities.Comment)(parameter as System.Array).GetValue(1);
            DataAccess.Entities.User user = Services.DataStorage.Instance.LoggedUser;
            DataAccess.Entities.CommentLike commentLike = photoInsideViewModel.UnitOfWork.CommentLikeRepository.TryGetUserLike(comment, user);

            // suitable for likes and dislikes
            // STEPS
            // 1. if like is new -> add it
            // 2. else if there is like or dislike
            //      2.1. if click on the same button type -> remove like
            //      2.2  else if click on the oposite button type -> toggle like

            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, isLike ? "Liking" : "Disliking");

            if (commentLike == null)// 1. add like
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Add like");
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Add to data base");

                // update data base
                DataAccess.Context.UnitOfWork.Instance.CommentLikeRepository.Insert(new DataAccess.Entities.CommentLike
                {
                    IsLiked = isLike,
                    Comment = comment,
                    User = user
                });
            }
            else// 2. there is a like
            {
                if (commentLike.IsLiked == isLike)// 2.1 remove like
                {
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Remove like");
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Update valuse in data base");

                    // updata data base
                    DataAccess.Context.UnitOfWork.Instance.CommentLikeRepository.Delete(commentLike);
                    DataAccess.Context.UnitOfWork.Instance.CommentRepository.Update(comment);
                }
                else// 2.2 toggle dislike to like
                {
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Toggle like");
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Update valuse in data base");

                    // updata data base
                    commentLike.IsLiked = isLike; // toggle
                    DataAccess.Context.UnitOfWork.Instance.CommentLikeRepository.Update(commentLike);
                }
            }

            // update interface, comment likes
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Update view");
            comment.PropertyUpdates(nameof(DataAccess.Entities.Comment.Likes));
            
            //  save changes to data base
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Save changes to data base");
            DataAccess.Context.UnitOfWork.Instance.Save();
        }
    }
}
