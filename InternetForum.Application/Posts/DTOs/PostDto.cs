using System;
using System.Collections.Generic;
using AutoMapper;
using InternetForum.Application.Comments.DTOs;
using InternetForum.Application.Common.Mappings;
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
		/// Vrací nebo nastavuje, kdy byl příspěvek přidán.
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje Id vlákna, kterého je příspěvek součástí.
		/// </summary>
		public int ForumThreadId { get; set; }
		
		/// <summary>
		/// Vrací nebo nastavuje název vlákna, kterého je příspěvek součástí.
		/// </summary>
		public string ForumThreadName { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje Id autora.
		/// </summary>
		public string AuthorId { get; set; }
		
		/// <summary>
		/// Vrací nebo nastavuje jméno autora.
		/// </summary>
		public string AuthorName { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje kolekci komentářů tohoto příspěvku.
		/// </summary>
		public IEnumerable<CommentDto> Comments { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Post, PostDto>()
				.ForMember(d => d.ForumThreadName, opt => opt.MapFrom(e => e.ForumThread.Name));
		}
    }
}
