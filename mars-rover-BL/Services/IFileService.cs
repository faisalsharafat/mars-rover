using Microsoft.AspNetCore.Http;

namespace mars_rover_BL.Services
{
    public interface IFileService
    {
        bool Exists(string path);

        /// <summary>
        /// wild card file search. Works when only one file satisfies the filter. 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileNameStart"></param>
        /// <returns></returns>
        bool ExistsStartWith(string path, string fileNameStart);


        /// <summary>
        /// wild card file retrieval. Works when only one file satisfies the filter. 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileNameStart"></param>
        /// <returns></returns>
        byte[] GetStartWith(string path, string fileNameStart);


        /// <summary>
        /// Returns the line data of the specified file. 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string[] Get(string path);


        /// <summary>
        /// Returns the line data of the in-memory file. 
        /// </summary>
        /// <param name="file">In-memory file</param>
        /// <returns></returns>
        string[] Get(IFormFile file);
        void Delete(string path);
        bool DirectoryExists(string path);
        void CreateDirectory(string path);
        string[] GetFileNames(string path);
        string[] GetDirectoryNames(string path);
        void SaveFile(string path, byte[] fileContent);
        void ResizeImageFile(string filePath, string thumpPath, int width, int height);

    }
}
