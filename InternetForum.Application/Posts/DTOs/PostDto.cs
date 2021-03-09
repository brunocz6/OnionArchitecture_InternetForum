using System.Collections.Generic;
using InternetForum.Application.Comments.DTOs;
using InternetForum.Application.Common.Mappings;
using InternetForum.Application.ForumThreads.DTOs;
using InternetForum.Application.Users.DTOs;
using InternetForum.Domain.Entities;
using InternetForum.Domain.Interfaces;

namespace InternetForum.Application.Posts.DTOs
{
    public class PostDto : IMapFrom<Post>, IHasKey<int>
    {
		/// <summary>
		/// Vrací nebo nastavuje Id vlákna.
		/// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje nadpis příspěvku.
        /// </summary>
        public string Title { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje obsah příspěvku.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje Id vlákna, kterého je příspěvek součástí.
		/// </summary>
		public int ForumThreadId { get; set; }
		
		/// <summary>
		/// Vrací nebo nastavuje, kterého vlákna je příspěvek součástí.
		/// </summary>
		public virtual ForumThreadDto ForumThread { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje Id autora.
		/// </summary>
		public string AuthorId { get; set; }
		
		/// <summary>
		/// Vrací nebo nastavuje autora.
		/// </summary>
		public UserDto Author { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje kolekci komentářů tohoto příspěvku.
		/// </summary>
		public virtual IEnumerable<CommentDto> Comments { get; set; }
    }
}
