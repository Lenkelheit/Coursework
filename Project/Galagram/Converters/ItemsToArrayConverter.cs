using System;
using System.Windows.Data;
using System.Globalization;

namespace Galagram.Converters
{
    /// <summary>
    /// Packs multiple items to array.
    /// <para/>
    /// Has been used to pack multiple parameters in one and passed it to <see cref="Galagram.ViewModel.Commands.User.PhotoInside.LikeCommentCommand"/>
    /// </summary>
    [ValueConversion(sourceType: typeof(object[]), targetType: typeof(object))]
    public class ItemsToArrayConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts multiple items to an array
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
            return values.Clone();
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
