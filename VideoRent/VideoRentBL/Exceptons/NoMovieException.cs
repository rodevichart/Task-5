using System;

namespace VideoRentBL.Exceptons
{
    [Serializable]
    public class NoMovieException : BlException
    {
        public NoMovieException()
        {
        }

        public NoMovieException(string message)
            : base(message)
        {
        }

        public NoMovieException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}