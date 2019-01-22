using System;
using System.Linq;
using System.Windows.Data;
using System.Globalization;
using System.Collections.Generic;

namespace Galagram.Converters
{
    /// <summary>
    /// Converts comment collection to number, depending how much there is like or dislike
    /// <para/>
    /// Has been used in TextBox={Binding} in comment likes/dislikes amount
    /// </summary>
    [ValueConversion(sourceType: typeof(ICollection<DataAccess.Entities.CommentLike>), targetType: typeof(int))]
    public class CommentCollectionToLengthConverter : IValueConverter
    {
        /// <summary>
        /// Turn collection to its size
        /// </summary>
        /// <param name="value">
        /// Object thet inherit ICollection interface
        /// </param>
        /// <param name="targetType">
        /// The type of the binding target property.
        /// </param>
        /// <param name="parameter">
        /// The converter parameter to use.
        /// <para/>
        /// True if count like, false if count dislike
        /// </param>
        /// <param name="culture">
        /// The culture to use in the converter
        /// </param>
        /// <returns>
        /// True if user in follow list is logged user
        /// <para/>
        /// False if user in follow list is not logged user, or casting operation went wrong
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((ICollection<DataAccess.Entities.CommentLike>)value).Count(c => c.IsLiked == (bool)parameter);
        }
        /// <summary>
        /// Not expected behavior.
        /// </summary>
        /// <param name="value">The value that the binding target produces.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns> A values that have been converted from the target value back to the source value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
