using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FindJob.Extentions
{
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

        public async static Task<string> SavePdf(this IFormFile file, string root, string folder)
        {
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string resultPath = Path.Combine(root, folder, fileName);

            using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }
    }
}
