using System;

namespace VideoRentBL.Exceptons
{
    public class NoMovieException : Exception
    {
        public NoMovieException(string message)
            :base(message)
        {
        }
    }
}