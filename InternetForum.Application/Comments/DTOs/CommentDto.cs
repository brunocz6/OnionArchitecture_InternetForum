using System;
using InternetForum.Application.Common.Mappings;
using InternetForum.Application.Posts;
using InternetForum.Application.Posts.DTOs;
using InternetForum.Application.Users.DTOs;
using InternetForum.Domain.Entities;
using InternetForum.Domain.Interfaces;

namespace InternetForum.Application.Comments.DTOs
{
    public class CommentDto : IMapFrom<Comment>, IHasKey<int>
    {
		/// <summary>
		/// Vrací nebo nastavuje Id komentáře.
		/// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje text komentáře.
        /// </summary>
        public string Body { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje datum a čas, kdy byl komentář vytvořen.
		/// </summary>
		public DateTime CreatedAt { get; set; }
		
		/// <summary>
		/// Vrací nebo nastavuje id autora.
		/// </summary>
		public string AuthorId { get; set; }
		
		/// <summary>
		/// Vrací nebo nastavuje autora.
		/// </summary>
		public UserDto Author { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje Id příspěvku.
		/// </summary>
		public int PostId { get; set; }
		
		/// <summary>
		/// Vrací nebo nastavuje příspěvek, pod který komentář patří.
		/// </summary>
		public PostDto Post { get; set; }
    }
}
