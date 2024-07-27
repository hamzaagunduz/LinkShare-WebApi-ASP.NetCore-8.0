using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Exceptions
{
    public class TransactionException : Exception
    {
        public TransactionException(string message) : base(message)
        {
        }
        public TransactionException() : base("TransactionException")
        {
        }
    }
}
