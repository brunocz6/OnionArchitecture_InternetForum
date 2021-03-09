using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetForum.Models
{
	public class LinkViewModel
	{
		public LinkViewModel()
		{

		}

		public LinkViewModel(int id, string text)
		{
			this.Id = id.ToString();
			this.Text = text;
		}

		public LinkViewModel(string id, string text)
		{
			this.Id = id;
			this.Text = text;
		}

		/// <summary>
		/// Vrací nebo nastavuje Id parametr.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje text odkazu.
		/// </summary>
		public string Text { get; set; }
	}
}