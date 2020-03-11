namespace mars_rover_BL.Services
{
    public interface IFileService
    {
        bool Exists(string path);
        bool ExistsStartWith(string path, string fileNameStart);
        byte[] GetStartWith(string path, string fileNameStart);
        string[] Get(string path);
        void Delete(string path);
        bool DirectoryExists(string path);
        void CreateDirectory(string path);
        string[] GetFileNames(string path);
        string[] GetDirectoryNames(string path);
        void SaveFile(string path, byte[] fileContent);
        void ResizeImageFile(string filePath, string thumpPath, int width, int height);

    }
}
