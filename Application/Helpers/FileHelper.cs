using Microsoft.AspNetCore.Http;

namespace SocialMedia_App.Core.Application.Helpers
{
    public class FileHelper
    {
        // image upload
        public string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            // directorio
            string basePath = $"/Images/Users/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            // crea folder si no existe
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // ruta del archivo
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);

            // nombre que tendrá la imagen cuando se suba a mi servidor
            string fileName = guid + fileInfo.Extension;

            // combinación de la ruta completa y el nombre de la imagen que formé
            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Path.Combine(basePath, fileName);

        }
    }
}
