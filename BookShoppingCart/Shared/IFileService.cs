namespace BookShoppingCart.Shared
{
    public interface IFileService
    {
        void DeleteFile(string fileName);
        Task<string> SaveFile(IFormFile file, string[] allowedExtensions);
    }
}
