using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    /// <summary>
    /// Contains logic to check exceptions.
    /// </summary>
    public static class ExceptionAssert
    {
        /// <summary>
        /// Checks if the input delegate throws a exception of type TException.
        /// </summary>
        /// <typeparam name="TException">The type of exception expected.</typeparam>
        /// <param name="methodToExecute">The method to execute to generate the exception.</param>
        public static void Throws<TException>(Action methodToExecute) where TException : Exception
        {
            try
            {
                methodToExecute();
                Assert.Fail("Expected exception of type " + typeof(TException) + " but no exception was thrown.");
            }
            catch (TException)
            {
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected exception of type " + typeof(TException) + " but type of " + ex.GetType() + " was thrown instead.");
            }
        }
    }
}
