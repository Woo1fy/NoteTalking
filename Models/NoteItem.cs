using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTalking.Models
{
	public class NoteItem
	{
		public int Id { get; set; }
		public string Title { get; set; } = null!;
		public string? Text { get; set; }
		public List<Content> Contents { get; set; } = new List<Content>();
		
		public int Order {get; set;}
	}
	
	public class Content
	{
	   // public string Type { get; set; }
	   public int Id { get; set; }
	   public byte[] Data { get; set; } = null!;
	}
}
