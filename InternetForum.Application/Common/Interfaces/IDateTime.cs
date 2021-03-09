using System;

namespace InternetForum.Application.Common.Interfaces
{
    /// <summary>
    /// Rozhraní pro službu poskytující aktuální čas.
    /// </summary>
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}
