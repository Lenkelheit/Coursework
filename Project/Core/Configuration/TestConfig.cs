namespace Core.Configuration
{
    /// <summary>
    /// Consists of all configuration needed in unit test
    /// </summary>
    public class TestConfig
    {
        /// <summary>
        /// Determines if need to delete test database
        /// </summary>
        public static readonly bool DO_DELETE_TEST_DATABASE = false;

        /// <summary>
        /// Specify test mode for DataBase
        /// </summary>
        public static readonly Enums.DataBaseFillMode DATABASE_FILL_MODE = Enums.DataBaseFillMode.Regular;
    }
}
