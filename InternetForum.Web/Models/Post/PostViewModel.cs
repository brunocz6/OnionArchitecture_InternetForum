using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using InternetForum.Application.Common.Interfaces;
using InternetForum.Application.Posts.DTOs;
using InternetForum.Models;
using InternetForum.Web.Models.Comment;
using MediatR;

namespace InternetForum.Web.Models.Post
{
	public class PostViewModel
	{
		/// <summary>
		/// Vrací nebo nastavuje ID příspěvku.
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
		/// Vrací nebo nastavuje odkaz autora příspěvku.
		/// </summary>
		public LinkViewModel AuthorLink { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje datum přidání příspěvku.
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje odkaz na vlákno příspěvku.
		/// </summary>
		public LinkViewModel ForumThreadLink { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje komentáře příspěvku.
		/// </summary>
		public IEnumerable<CommentViewModel> Comments { get; set; }

		/// <summary>
		/// Vytváří a vrací novou instanci <see cref="PostViewModel"/> modelu s vyplněnými daty z entity příspěvku.
		/// </summary>
		/// <param name="post">Entita příspěvku</param>
		/// <param name="commentsPageNumber">Číslo stránky komentářů</param>
		/// <param name="commentsPageSize">Počet komentářů na jedné stránce</param>
		public static PostViewModel FromDto(PostDto post)
		{
			var model = new PostViewModel()
			{
				Id = post.Id,
				Title = post.Title,
				Body = post.Body,
				CreatedAt = post.CreatedAt,
				Comments = post.Comments.Select(CommentViewModel.FromDto),
				AuthorLink = new LinkViewModel(post.AuthorId, post.AuthorName),
				ForumThreadLink = new LinkViewModel(post.ForumThreadId, post.ForumThreadName),
			};

			return model;
		}
	}
}