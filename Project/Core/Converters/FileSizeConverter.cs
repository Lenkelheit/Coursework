namespace Core.Converters
{
    /// <summary>
    /// Uses for converting file size to bytes.
    /// </summary>
    public static class FileSizeConverter
    {
        /// <summary>
        /// Converts file size in file size mode to bytes.
        /// </summary>
        /// <param name="size">The file size in file size mode.</param>
        /// <param name="fileSizeMode">The kind of file size.</param>
        /// <returns>
        /// Returns file size in bytes.
        /// </returns>
        public static long GetSize(long size, Enums.FileSizeMode fileSizeMode)
        {
            long basis2to10power = 1024;
            for (Enums.FileSizeMode i = 0; i <= fileSizeMode; ++i) 
            {
                size *= basis2to10power;
            }
            return size;
        }
    }
}
