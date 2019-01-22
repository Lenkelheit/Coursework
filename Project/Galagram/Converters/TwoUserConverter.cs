using System;
using System.Windows.Data;
using System.Globalization;

using DataAccess.Entities;

namespace Galagram.Converters
{
    /// <summary>
    /// Compares two users.
    /// <para/>
    /// Very specific converter
    /// <para/>
    /// Has been used in follow list, to show delete button, when logged user visit other user pagges
    /// </summary>
    public class TwoUserCompareConverter : IMultiValueConverter
    {
        /// <summary>
        /// Covert two users to boolean value in way comparing them.
        /// </summary>
        /// <param name="values">
        /// Two users. <para/>
        /// 1 - user from follow list <para/>
        /// 2 - logged user
        /// </param>
        /// <param name="targetType">
        /// The type of the binding target property.
        /// </param>
        /// <param name="parameter">
        /// The converter parameter to use.
        /// </param>
        /// <param name="culture">
        /// The culture to use in the converter
        /// </param>
        /// <returns>
        /// True if user in follow list is logged user
        /// <para/>
        /// False if user in follow list is not logged user, or casting operation went wrong
        /// </returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            User user1 = values[0] as User;// user from follow list
            User user2 = (User)values[1];// logged user

            if (user1 == null) return false; // disconnected item

            return user1 == user2;
        }
        /// <summary>
        /// Not expected behavior.
        /// </summary>
        /// <param name="value">The value that the binding target produces.</param>
        /// <param name="targetTypes">The array of types to convert to. The array length indicates the number and types of values that are suggested for the method to return.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns> An array of values that have been converted from the target value back to the source values.</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
