﻿using System.Windows.Input;
using System.ComponentModel;

namespace Galagram.ViewModel.ViewModel.User
{
    public class PhotoInsideViewModel : ViewModelBase
    {
        // CONSTRUCTORS
        public PhotoInsideViewModel()
        {
            throw new System.NotImplementedException();
        }
        // PROPERTIES
        public DataAccess.Entities.Photo Photo
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
        public int SelectedCommentIndex
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
        public DataAccess.Entities.Comment[] Comments
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
        public string Text
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
        // COMMAND
        public ICommand LikePhotoCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public ICommand DisLikePhotoCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public ICommand LikeCommentCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public ICommand DisLikeCommentCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public ICommand WriteCommentCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public ICommand DeleteCommentCommand
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
