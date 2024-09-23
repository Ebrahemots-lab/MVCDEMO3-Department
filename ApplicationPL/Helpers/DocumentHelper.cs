namespace ApplicationPL.Helpers
{
    public static class DocumentHelper
    {

        public static string Upload(IFormFile file, string folderName)
        {
            //1. Get Located Folder Path 
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);
            //2. Get File Name and Make it Unique 
            string fileName = $"{Guid.NewGuid()}{file.FileName}";
            //3. Get File Path [Folder Path + FileName]
            string filePath = Path.Combine(folderPath, fileName);
            //4. Save File As Streams
            using var fileStream = new FileStream(filePath,FileMode.Create);
            file.CopyTo(fileStream);
            //5. Return File Name 
            return fileName;
        }

    }
}
