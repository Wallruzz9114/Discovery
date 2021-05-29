using System;

namespace Models.Base
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
    }
}