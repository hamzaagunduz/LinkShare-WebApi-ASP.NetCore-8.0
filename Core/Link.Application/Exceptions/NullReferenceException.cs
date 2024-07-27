using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Exceptions
{
    public class NullReferenceException : Exception
    {
        public NullReferenceException(string message) : base(message)
        {
        }
        public NullReferenceException() : base("NullReferanceException")
        {
        }
    }
}
