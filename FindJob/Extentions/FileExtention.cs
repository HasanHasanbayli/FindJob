using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FindJob.Extentions;

public static class FileExtention
{
    public static bool IsPdf(this IFormFile file)
    {
        return file.ContentType.Contains("pdf/");
    }

    public static bool MaxLengthPdf(this IFormFile file, int kb)
    {
        return file.Length / 1024 > kb;
    }

    public static async Task<string> SavePdf(this IFormFile file, string root, string folder)
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