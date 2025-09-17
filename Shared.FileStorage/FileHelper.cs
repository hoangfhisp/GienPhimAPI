using Microsoft.AspNetCore.Http;

namespace Shared.FileStorage
{
    // Lưu trữ file , đọc , xóa file
    public static class FileHelper
    {
        // Lưu ảnh vào thư mục và trả về đường dẫn file để lưu vào DB
        public static async Task<string> SaveImageAsync(IFormFile file, string folderPath)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File không hợp lệ");

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var savePath = Path.Combine(folderPath, fileName);

            Directory.CreateDirectory(folderPath);

            // Nếu thư mục chưa có thì tự động tạo.
            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName; // trả về tên file để lưu DB
        }

        // Xóa Ảnh theo đường dẫn tương đối
        public static bool DeleteImage(string relativePath) // Đường dẫn tương đối
        {
            if (string.IsNullOrEmpty(relativePath))
                return false;
            
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }
    }
}
