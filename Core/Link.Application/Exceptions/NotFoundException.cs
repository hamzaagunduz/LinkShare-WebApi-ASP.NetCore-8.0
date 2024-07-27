using System;

namespace Link.Application.Exceptions
{
    public class NotFoundException : Exception
    {

        public NotFoundException() : base("Bulunamadı")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

    }
}
