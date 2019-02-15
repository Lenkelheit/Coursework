using System;
using System.Windows.Data;
using System.Globalization;

using DataAccess.Wrappers;

namespace Galagram.Converters
{
    /// <summary>
    /// Compares users
    /// <para/>
    /// Very specific converter
    /// <para/>
    /// Has been used in follow list, to show delete button, when logged user visit other user pages
    /// <para/>
    /// Has been used in comment section, to show delete button, when logged user visit other user pages
    /// </summary>
    [ValueConversion(sourceType: typeof(UserWrapper[]), targetType: typeof(bool))]
    public class UserCompareConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts two users to boolean value in way comparing them.
        /// </summary>
        /// <param name="values">
        /// Two users. <para/> 
        /// 1 - logged user <para/>
        /// 2 - user from follow list or from comment section <para/>
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
            UserWrapper user1 = (UserWrapper)values[0];// logged user
            UserWrapper user2 = values[1] as UserWrapper;// user from follow list or from comment section

            if (user2 == null) return false; // disconnected item

            return user2 == user1;
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
