using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBanking.Infrastructure
{
    /// <summary>
    /// Replacing Throwing Exceptions with Notification in Validations (Martin Fowler, 09 December 2014)
    /// See: <a href="https://martinfowler.com/articles/replaceThrowWithNotification.html">Martin Fowler's article</a>
    /// </summary>
    public class Notification
    {
        private readonly List<Error> _errors = new();

        public void AddError(string message, Exception? e)
            => _errors.Add(new Error(message, e!));

        public void AddError(string message) => AddError(message, null);

        public bool HasErrors => _errors.Count > 0;

        public string ErrorMessage
            => string.Join(", ", _errors.Select(e => e.Message));

        public List<string> Errors
        {
            get
            {
                return _errors.Select(e => e.Message).ToList();
            }
        }

        private class Error
        {
            internal string Message { get; }

            internal Exception Cause { get; }

            internal Error(string message, Exception cause)
            {
                Message = message;
                Cause = cause;
            }
        }
    }
}