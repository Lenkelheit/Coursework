using Microsoft.VisualStudio.TestTools.UnitTesting;

using Galagram.ViewModel.Commands;
using DataAccess.Entities;

namespace UnitTest.ViewModel.Commands
{
    [TestClass]
    public class RelayCommandTest
    {
        // TESTS
        [TestMethod]
        public void CanExecuteImplicitNull()
        {
            // Arrange
            CommentLike commentLike = new CommentLike
            {
                IsLiked = true
            };
            RelayCommand relayCommand = new RelayCommand(o =>
            {
                CommentLike changeCommentLike = o as CommentLike;
                if (changeCommentLike != null) changeCommentLike.IsLiked = false;
            });
            bool expectedCanExecute = true;

            // Act
            bool actualCanExecute = relayCommand.CanExecute(commentLike);

            // Assert
            Assert.AreEqual(expectedCanExecute, actualCanExecute);
        }
        [TestMethod]
        public void CanExecuteExplicitNull()
        {
            // Arrange
            CommentLike commentLike = new CommentLike
            {
                IsLiked = true
            };
            RelayCommand relayCommand = new RelayCommand
            (
                execute: o =>
                {
                    CommentLike changeCommentLike = o as CommentLike;
                    if (changeCommentLike != null) changeCommentLike.IsLiked = false;
                },
                canExecute: null
            );
            bool expectedCanExecute = true;

            // Act
            bool actualCanExecute = relayCommand.CanExecute(commentLike);

            // Assert
            Assert.AreEqual(expectedCanExecute, actualCanExecute);
        }
        [TestMethod]
        public void CanExecuteExplicitTrue()
        {
            // Arrange
            CommentLike commentLike = new CommentLike
            {
                IsLiked = true
            };
            RelayCommand relayCommand = new RelayCommand
            (
                execute: o =>
                {
                    CommentLike changeCommentLike = o as CommentLike;
                    if (changeCommentLike != null) changeCommentLike.IsLiked = false;
                },
                canExecute: o => (o as CommentLike).IsLiked == true
            );
            bool expectedCanExecute = true;

            // Act
            bool actualCanExecute = relayCommand.CanExecute(commentLike);

            // Assert
            Assert.AreEqual(expectedCanExecute, actualCanExecute);
        }
        [TestMethod]
        public void CanExecuteExplicitFalse()
        {
            // Arrange
            CommentLike commentLike = new CommentLike
            {
                IsLiked = true
            };
            RelayCommand relayCommand = new RelayCommand
            (
                execute: o =>
                {
                    CommentLike changeCommentLike = o as CommentLike;
                    if (changeCommentLike != null) changeCommentLike.IsLiked = false;
                },
                canExecute: o => (o as CommentLike).IsLiked == false
            );
            bool expectedCanExecute = false;

            // Act
            bool actualCanExecute = relayCommand.CanExecute(commentLike);

            // Assert
            Assert.AreEqual(expectedCanExecute, actualCanExecute);
        }
        [TestMethod]
        public void Execute()
        {
            // Arrange
            CommentLike commentLike = new CommentLike
            {
                IsLiked = true
            };
            RelayCommand relayCommand = new RelayCommand(o =>
            {
                CommentLike changeCommentLike = o as CommentLike;
                if (changeCommentLike != null) changeCommentLike.IsLiked = false;
            });
            bool expectedCommentLikeIsLiked = false;

            // Act
            relayCommand.Execute(commentLike);

            // Assert
            Assert.AreEqual(expectedCommentLikeIsLiked, commentLike.IsLiked);
        }
    }
}
