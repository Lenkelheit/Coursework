namespace Galagram.ViewModel.Commands.User.PhotoInside
{
    /// <summary>
    /// Sets like or dislike to current photo
    /// </summary>
    public class LikePhotoCommand : CommandBase
    {
        // FIELDS
        ViewModel.User.PhotoInsideViewModel photoInsideViewModel;

        // CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of <see cref="LikePhotoCommand"/>
        /// </summary>
        /// <param name="photoInsideViewModel">
        /// An instance of <see cref="ViewModel.User.PhotoInsideViewModel"/>
        /// </param>
        public LikePhotoCommand(ViewModel.User.PhotoInsideViewModel photoInsideViewModel)
        {
            this.photoInsideViewModel = photoInsideViewModel;
        }

        // METHODS
        /// <summary>
        /// Checks if command can be executed
        /// </summary>
        /// <param name="parameter">
        /// Additionals parameters
        /// </param>
        /// <returns>
        /// True if command can be executed, otherwise — false
        /// </returns>
        public override bool CanExecute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Can execute {nameof(LikePhotoCommand)}");
            return true;
        }
        /// <summary>
        /// Executes command
        /// </summary>
        /// <param name="parameter">
        /// Command parameters
        /// <para/>
        /// 1 — bool, is like or dislike
        /// </param>
        public override void Execute(object parameter)
        {
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, $"Execute {nameof(LikePhotoCommand)}");
            
            // gets value
            bool isLike = System.Convert.ToBoolean(parameter);
            DataAccess.Entities.Photo photo = photoInsideViewModel.Photo;
            DataAccess.Entities.User user = Services.DataStorage.Instance.LoggedUser;
            DataAccess.Entities.PhotoLike photoLike = photoInsideViewModel.UnitOfWork.PhotoLikeRepository.TryGetUserLike(photo, user);

            // suitable for likes and dislikes
            // STEPS
            // 1. if like is new -> add it
            // 2. else if there is like or dislike
            //      2.1. if click on the same button type -> remove like
            //      2.2  else if click on the opposite button type -> toggle like

            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, isLike ? "Liking" : "Disliking");

            if (photoLike == null)// 1. add like
            {
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Add like");
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Add to data base");

                // update data base
                DataAccess.Context.UnitOfWork.Instance.PhotoLikeRepository.Insert(new DataAccess.Entities.PhotoLike
                {
                    IsLiked = isLike,
                    Photo = photo,
                    User = user
                });

                // update view
                Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Update View");
                if (isLike) ++photoInsideViewModel.LikeDislikeAmount.LikesAmount;
                else        ++photoInsideViewModel.LikeDislikeAmount.DisLikesAmount;

                // liked
                photoInsideViewModel.LikeValue = isLike;
            }
            else// 2. there is a like
            {
                if(photoLike.IsLiked == isLike)// 2.1 remove like
                {
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Remove like");
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Update valuse in data base");

                    // updata data base
                    DataAccess.Context.UnitOfWork.Instance.PhotoLikeRepository.Delete(photoLike);
                    DataAccess.Context.UnitOfWork.Instance.PhotoRepository.Update(photo);

                    // update view
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Update View");
                    if (isLike) --photoInsideViewModel.LikeDislikeAmount.LikesAmount;
                    else        --photoInsideViewModel.LikeDislikeAmount.DisLikesAmount;

                    // same button, remove
                    photoInsideViewModel.LikeValue = null;
                }
                else// 2.2 toggle dislike to like
                {
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Toggle like");
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Update valuse in data base");

                    // updata database
                    photoLike.IsLiked = isLike; // toggle
                    DataAccess.Context.UnitOfWork.Instance.PhotoLikeRepository.Update(photoLike);

                    // update view, toggle value
                    Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Update View");
                    if (isLike)
                    {
                        ++photoInsideViewModel.LikeDislikeAmount.LikesAmount;
                        --photoInsideViewModel.LikeDislikeAmount.DisLikesAmount;
                    }
                    else
                    {
                        --photoInsideViewModel.LikeDislikeAmount.LikesAmount;
                        ++photoInsideViewModel.LikeDislikeAmount.DisLikesAmount;
                    }

                    // toggle like
                    photoInsideViewModel.LikeValue = isLike;
                }
            }

            // notify view
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Update view. Raise event");
            photoInsideViewModel.UpdateLikes();

            // save changes to database
            Core.Logger.GetLogger.LogAsync(Core.LogMode.Debug, "Save changes to data base");
            DataAccess.Context.UnitOfWork.Instance.Save();
        }
    }
}
