using System;

namespace Application.Exceptions
{
    /// <summary>The base class for exceptions which can be triggered by bad user input</summary>
    public abstract class UserExceptionBase : Exception
    {
        protected UserExceptionBase(string message) : base(message) { }
    }
}
