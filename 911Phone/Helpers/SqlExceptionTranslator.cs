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
                        case 3:
                        case 7:
                        case 8:
                        
                        case 2:
                        case 4:
                        case 5:
                        case 10:
                        case 11:
                            throw new ArgumentOutOfRangeException(exceptionDescription);
                        case 12:
                            throw new Exceptions.CurrentEntryNotFoundException(exceptionDescription);
                        case 13:
                        case 14:
                        default:
                            throw new InvalidProgramException("Uknown SQL Exception catched " + message, ex);
                    }
                default:
                    throw new InvalidProgramException("Uknown SQL Exception catched ", ex);
            }
        }
    }
}
