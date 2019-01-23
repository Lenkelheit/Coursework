using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Galagram.Converters
{
    /// <summary>
    /// Сonverts string to uncached bitmap image
    /// <para/>
    /// Has been used on Avatar and TempSetting image
    /// </summary>
    [ValueConversion(sourceType: typeof(string), targetType: typeof(BitmapImage))]
    public class ImageConverter: IValueConverter
    {
        /// <summary>
        /// Converts image to  uncached bitmap image
        /// </summary>
        /// <param name="value">
        /// A string path to image
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
        /// Bitmap image if value is valid, otherwise — null
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            string imagePath = value.ToString();

            // path is not empty and file exist
            if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(imagePath);
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                image.EndInit();

                return image;
            }

            return null;
        }
        /// <summary>
        /// Not expected behavior.
        /// </summary>
        /// <param name="value">The value that the binding target produces.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns> A values that have been converted from the target value back to the source value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
