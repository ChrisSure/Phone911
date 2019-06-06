using Phone.Exceptions.Catalog;
using Phone.Exceptions.User;
using System;
using System.Data.SqlClient;

namespace Phone.Helpers
{
    public class SqlExceptionTranslator
    {
        /// <summary>
        /// Generate approriate exception from SqlException
        /// </summary>
        /// <param name="ex">SqlException</param>
        /// <param name="message">Addtitional message contactane to default</param>
        public static void ReThrow(SqlException ex, string message = "")
        {
            switch (ex.Number)
            {
                case 2601: // Unique constraint error
                    throw new UserUniqueConstrainException("Entered email already used!");
                case 2627:
                    throw new UserUniqueConstrainException("Entered email already used!");
                case 207: // Invalid column name
                case 2812: // Could not find stored procedure
                    throw new InvalidProgramException("Operation failed because of a program error. " + ex.Message, ex);
                case 50001:
                    var exceptionDescription = ex.Message + " " + message;
                    switch (ex.State)
                    {
                        case 1:
                            throw new Exceptions.CurrentEntryNotFoundException(exceptionDescription);
                        case 2:
                            throw new ArgumentOutOfRangeException(ex.Message);
                        case 3:
                            throw new IssetChildException(exceptionDescription);
                        case 4:
                            throw new CircularCategoryException(exceptionDescription);
                        case 5:
                            throw new LevelCategoryException(exceptionDescription);
                        case 6:
                            throw new SortCategoryException(exceptionDescription);
                        default:
                            throw new InvalidProgramException("Uknown SQL Exception catched " + message, ex);
                    }
                default:
                    throw new InvalidProgramException("Uknown SQL Exception catched ", ex);
            }
        }
    }
}
