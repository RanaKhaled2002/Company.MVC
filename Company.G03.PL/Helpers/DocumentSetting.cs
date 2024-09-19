namespace Company.G03.PL.Helpers
{
    public static class DocumentSetting
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files",folderName);

            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            string filePath = Path.Combine(folderPath, fileName);

            var fileStream = new FileStream(filePath,FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
        }
    }
}
