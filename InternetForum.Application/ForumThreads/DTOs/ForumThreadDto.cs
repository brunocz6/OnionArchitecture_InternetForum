using InternetForum.Application.Common.Mappings;
using InternetForum.Application.Posts;
using InternetForum.Domain.Entities;
using InternetForum.Domain.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using InternetForum.Application.Posts.DTOs;

namespace InternetForum.Application.ForumThreads.DTOs
{
    public class ForumThreadDto : IMapFrom<ForumThread>, IHasKey<int>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id vlákna.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje název vlákna.
        /// </summary>
        public string Name { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje popis vlákna.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje příspěvky, které jsou součástí tohoto vlákna.
		/// </summary>
		public IEnumerable<PostDto> Posts { get; set; }
    }
}
