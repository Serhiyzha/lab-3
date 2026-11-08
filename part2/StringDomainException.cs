using System;

namespace StringApp.BLL
{
    public class StringDomainException : Exception
    {
        public StringDomainException(string message) : base(message) { }
    }
}