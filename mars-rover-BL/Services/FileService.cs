using System.IO;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace mars_rover_BL.Services
{
    public class FileService : IFileService
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// wild card file search. Works when only one file satisfies the filter. 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileNameStart"></param>
        /// <returns></returns>
        public bool ExistsStartWith(string path, string fileNameStart)
        {
            var files = Directory.EnumerateFiles(path, fileNameStart);
            return files != null && files.Count() == 1;
        }


        /// <summary>
        /// wild card file retrieval. Works when only one file satisfies the filter. 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileNameStart"></param>
        /// <returns></returns>
        public byte[] GetStartWith(string path, string fileNameStart)
        {
            byte[] fileBytes = null;
            try
            {
                var files = Directory.EnumerateFiles(path, fileNameStart);
                if (files != null && files.Count() == 1)
                {
                    var file = files.FirstOrDefault();
                    fileBytes = System.IO.File.ReadAllBytes(Path.Combine(path, file));
                }
            }
            catch
            {
            }

            return fileBytes;
        }


        /// <summary>
        /// Returns the line data of the specified file. 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string[] Get(string path)
        {
            string[] content = null;
            try
            {
                content = System.IO.File.ReadAllLines(path);
            }
            catch
            {
            }

            return content;
        }

        public void Delete(string path)
        {
            File.Delete(path);
        }


        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public string[] GetFileNames(string path)
        {
            return Directory.GetFiles(path).Select(x => Path.GetFileNameWithoutExtension(x)).ToArray(); ;
        }

        public string[] GetDirectoryNames(string path)
        {
            return Directory.GetDirectories(path).Select(x => Path.GetFileName(x) ).ToArray();
        }

        public void SaveFile(string path, byte[] fileContent)
        {
            File.WriteAllBytes(path, fileContent);
        }

        public void ResizeImageFile(string filePath, string thumpPath, int width, int height)
        {
            using (Image image = Image.Load(filePath))
            {
                image.Mutate(x => x
                     .Resize(width, height));
                image.Save(thumpPath); // Automatic encoder selected based on extension.
            }
        }

    }
}
