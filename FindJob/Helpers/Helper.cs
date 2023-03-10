using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Recruitment.Helpers;

public static class Helper
{
    public static void DeleteImage(string root, string folder, string fileName)
    {
        string filePath = Path.Combine(root, folder, fileName);
        if (File.Exists(filePath)) File.Delete(filePath);
    }

    internal static void DeleteImage(string webRootPath, string path, IFormFile photo)
    {
        throw new NotImplementedException();
    }
}