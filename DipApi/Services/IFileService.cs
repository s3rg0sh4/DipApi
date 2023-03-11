using DipApi.Entities;

namespace DipApi.Services
{
	public interface IFileService
	{
		void SaveFiles(List<IFormFile> files, string userId);
		Task<List<FileDetails>> GetUserFiles(User user);
	}
}
