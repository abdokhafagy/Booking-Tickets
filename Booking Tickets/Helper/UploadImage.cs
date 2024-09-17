namespace Booking_Tickets.Helper;
//using Microsoft.Extensions.Hosting;
public class UploadImage
{



    public static async Task<string> UploadImageAsync(IFormFile file, string webRootPath, string folderName)
    {
        if (file != null && file.Length > 0)
        {
            // Ensure the folder exists in wwwroot
            string folderPath = Path.Combine(webRootPath, folderName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Generate a unique file name to avoid conflicts
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Create the full file path
            string fullPath = Path.Combine(folderPath, fileName);

            // Save the file to the specified path
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Return the relative path to save in the database or use later
            return fileName;
        }

        return null;
    }

}

