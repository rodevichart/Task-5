using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentBL.Exceptons
{
    [Serializable]
    public class BlException : Exception
    {
        public BlException()
        {
        }

        public BlException(string message)
            : base(message)
        {
        }

        public BlException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
