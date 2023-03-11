using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DipApi.Entities
{
	public class FileDetails
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string FileName { get; set; }
		public byte[] FileData { get; set; }
		public string FileType { get; set; }
		public string UserId { get; set; }
		public virtual User User { get; set; }

	}
}
