using FilmBox.Api.DataAccess;
using FilmBox.Api.DTOs.GetDTOs;
using FilmBox.Api.DTOs.PostDTOs;
using FilmBox.Api.Models;
using FilmBox.Shared.DTOs.PostDTOs;

namespace FilmBox.Api.BusinessLogic
{
    // Handles validation, mapping, and uses the repository for database access
    public class AdminMediaLogic : IAdminMediaLogic
    {
        private readonly IAdminMediaDAO _adminMediaDAO;
        private readonly IWebHostEnvironment _env;


        // Repository is injected through constructor
        public AdminMediaLogic(IAdminMediaDAO adminMediaDAO, IWebHostEnvironment env)
        {
            _adminMediaDAO = adminMediaDAO;
            _env = env;
        }

        public async Task<int> CreateMediaAsync(MediaCreateDto dto)
        {
            string? imagePath = null;

            if (dto.MediaType != "Movie" && dto.MediaType != "Series")
                throw new ArgumentException("MediaType must be either 'Movie' or 'Series'");

            if (dto.ImageUrl != null)
            {
                var filesFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    _env.WebRootPath,
                    "Files"
                );

                Directory.CreateDirectory(filesFolder);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.ImageUrl.FileName)}";
                var fullPath = Path.Combine(filesFolder, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await dto.ImageUrl.CopyToAsync(stream);

                
                imagePath = $"/Files/{fileName}";
            }
            var media = new Media
            {
                Title = dto.Title,
                Description = dto.Description,
                Genre = dto.Genre,
                ImageUrl = imagePath,
                MediaType = dto.MediaType,
                PublishDate = dto.PublishDate
            };

            return await _adminMediaDAO.InsertAsync(media);
        }
        public async Task UpdateImageAsync(int mediaId, IFormFile image)
        {
            if (image == null || image.Length == 0)
                throw new ArgumentException("Image is required");

            var fileExtension = Path.GetExtension(image.FileName);
            var fileName = $"{Guid.NewGuid()}{fileExtension}";

            var filesPath = Path.Combine(
                _env.WebRootPath,
                "Files"
            );

            if (!Directory.Exists(filesPath))
                Directory.CreateDirectory(filesPath);

            var fullPath = Path.Combine(filesPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await image.CopyToAsync(stream);

            var imageUrl = $"/Files/{fileName}";

            await _adminMediaDAO.UpdateImageAsync(mediaId, imageUrl);
        }
    }
}
