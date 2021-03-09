using System;

namespace InternetForum.Domain.Common
{
    /// <summary>
    /// Entita o které víme, kdy byla vytvořena případně naposledy upravena.
    /// </summary>
    public class AuditableEntity<TKey> : Entity<TKey>
    {
        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string DeletedBy { get; set; }
    }
}
