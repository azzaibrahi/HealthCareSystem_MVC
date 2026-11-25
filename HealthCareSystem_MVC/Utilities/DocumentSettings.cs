namespace HealthCareSystem_MVC.Utilities;

/// <summary>File staging helper from the MVC reference, adapted to cross-platform paths.</summary>
public static class DocumentSettings
{
    public static string UploadFile(IFormFile file, string folderName)
    {
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName);
        Directory.CreateDirectory(folderPath);

        var fileName = $"{Guid.NewGuid():N}-{Path.GetFileName(file.FileName)}";
        var filePath = Path.Combine(folderPath, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        file.CopyTo(stream);
        return fileName;
    }

    public static void DeleteFile(string fileName, string folderName)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName, fileName);

        if (File.Exists(filePath))
            File.Delete(filePath);
    }
}
