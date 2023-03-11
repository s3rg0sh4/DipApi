using System.Collections;

using DipApi.DB;
using DipApi.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DipApi.Services.Implementation
{
	public class FileService : IFileService
	{
		private readonly UserContext _userContext;
		
		private readonly UserManager<User> _userManager;

		public FileService(UserContext userContext, UserManager<User> userManager)
		{
			_userContext = userContext;
			_userManager = userManager;
		}

		public async Task<List<FileDetails>> GetUserFiles(User user)
		{
			var files = user.FileDetails.ToList();

			return files;
		}

		public void SaveFiles(List<IFormFile> files, string userId)
		{
			var allowedTypes = new List<string> { "application/pdf", "image/png", "image/jpeg" };

			foreach (var file in files)
			{
				if (!allowedTypes.Contains(file.ContentType))
				{
					throw new Exception(message: "ContentType is not allowed");
				}

				FileDetails details = new()
				{
					FileName = file.FileName,
					FileType = file.ContentType
				};

				using (var binaryReader = new BinaryReader(file.OpenReadStream()))
				{
					details.FileData = binaryReader.ReadBytes((int)file.Length);
				}

				_userContext.Add(details);
			}
		}
	}
}
