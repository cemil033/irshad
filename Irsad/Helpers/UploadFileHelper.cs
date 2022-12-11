namespace Irsad.Helpers
{
    public static class UploadFileHelper
    {
        public async static Task<string> UploadFile(IFormFile file) {
            var fs = new FileStream(@$"wwwroot/Uploads/{file.FileName}", FileMode.Create);
            await file.CopyToAsync(fs);
            return file.FileName; 
        }
    }
}
