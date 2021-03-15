using System;
using AutoMapper;
using InternetForum.Application.Common.Mappings;
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
		/// Vrací nebo nastavuje jméno autora.
		/// </summary>
		public string AuthorName { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje Id příspěvku.
		/// </summary>
		public int PostId { get; set; }
    }
}
