using System;

namespace InternetForum.Application.Common.Exceptions
{
    /// <summary>
    /// Chyba v situaci, kdy byl objektu zakázán přístup.
    /// </summary>
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException() : base() { }
    }
}
