using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
		[JsonIgnore]
		public virtual User User { get; set; }

	}
}
