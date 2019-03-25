using System;

namespace Brother.Bpac
{
    public class BpacException : Exception
    {
        public BpacException() : base()
        {

        }

        public BpacException(string message) : base(message)
        {
        }
    }
}
