using InternetForum.Domain.Interfaces;

namespace InternetForum.Application.Users.DTOs
{
    public class UserDto : IHasKey<string>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatele.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje uživatelské jméno.
        /// </summary>
        public string UserName { get; set; }
    }
}