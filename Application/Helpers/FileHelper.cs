using Microsoft.AspNetCore.Http;

namespace SocialMedia_App.Core.Application.Helpers
{
    public class FileHelper
    {
        // Método para cargar un archivo
        public string UploadFile(IFormFile file, int id, string folderName, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }

            if (folderName != "Users" && folderName != "Posts")
            {
                throw new ArgumentException("Nombre de carpeta inválido", nameof(folderName));
            }

            // Directorio dependiendo del tipo de entidad
            string basePath = $"/Images/{folderName}/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            // Crea el directorio si no existe
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Genera un nombre único para el archivo
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);

            // Nombre del archivo en el servidor
            string fileName = guid + fileInfo.Extension;

            // Ruta completa del archivo
            string fileNameWithPath = Path.Combine(path, fileName);

            // Guarda el archivo en el servidor
            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (File.Exists(completeImageOldPath))
                {
                    File.Delete(completeImageOldPath);
                }
            }

            return Path.Combine(basePath, fileName); // Devuelve la ruta del archivo
        }
    }

}
