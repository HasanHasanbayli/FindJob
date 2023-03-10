using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Recruitment.Extentions;

public static class Extention
{
    public static bool IsImage(this IFormFile file)
    {
        return file.ContentType.Contains("image/");
    }

    public static bool MaxLength(this IFormFile file, int kb)
    {
        return file.Length / 1024 > kb;
    }

    public static async Task<string> SaveImg(this IFormFile file, string root, string folder)
    {
        string fileName = Guid.NewGuid() + file.FileName;
        string resultPath = Path.Combine(root, folder, fileName);

        using (FileStream fileStream = new(resultPath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return fileName;
    }
}