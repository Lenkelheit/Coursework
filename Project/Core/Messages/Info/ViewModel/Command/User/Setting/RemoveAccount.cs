﻿namespace Core.Messages.Info.ViewModel.Command.User.Setting
{
    /// <summary>
    /// Consists of all messages thet happen in <see cref="RemoveAccount"/> command
    /// </summary>
    public static class RemoveAccount
    {
        /// <summary>
        /// Verify if user do want to delete his account
        /// </summary>
        public readonly static string DO_DELETE_ACCOUNT = "Are you sure you want to delete your account?";
    }
}
