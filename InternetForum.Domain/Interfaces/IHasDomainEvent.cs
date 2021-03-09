using InternetForum.Domain.Common;
using System.Collections.Generic;

namespace InternetForum.Domain.Interfaces
{
    /// <summary>
    /// Rozhraní pro objekt, který má na sobě kolekci událostí, které se mu staly.
    /// </summary>
    public interface IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }
}
