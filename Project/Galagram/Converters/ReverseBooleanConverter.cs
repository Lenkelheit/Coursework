using System;
using System.Windows.Data;

namespace Galagram.Converters
{
    /// <summary>
    /// Converts bool to its opposite value
    /// </summary>
    [ValueConversion(sourceType: typeof(bool), targetType: typeof(bool))]
    public class ReverseBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Converts bool to its opposite value
        /// </summary>
        /// <param name="value">
        /// A boolean value
        /// </param>
        /// <param name="targetType"> The type of the binding target property. </param>
        /// <param name="parameter"> The converter parameter to use. </param>
        /// <param name="culture"> The culture to use in the converter </param>
        /// <returns>
        /// Reversed boolean value
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !System.Convert.ToBoolean(value);           
        }
        /// <summary>
        /// Converts bool to its opposite value
        /// </summary>
        /// <param name="value">
        /// A boolean value
        /// </param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// Reversed boolean value
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !System.Convert.ToBoolean(value);
        }
    }
}
