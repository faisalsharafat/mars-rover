using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using mars_rover_models.DTO;
using mars_rover_models.ViewModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace mars_rover_BL.Services
{
    public class MarsRoverService : IMarsRoverService
    {
        private readonly IMapper _mapper;
        private readonly WepApiAppSettings _appSettings;
        private readonly IWebService webApiService;
        private readonly IFileService _fileWrapper;

        public MarsRoverService(IMapper mapper, IOptions<WepApiAppSettings> appSettings, IFileService fileWrapper, IWebService webApiService)
        {
            this._mapper = mapper;
            this._appSettings = appSettings.Value;
            this.webApiService = webApiService;
            this._fileWrapper = fileWrapper;
        }


        /// <summary>
        /// Get the already downloaded rover image dates as a list.
        /// </summary>
        /// <returns></returns>
        public DateFolders GetDateFolders()
        {
            try
            {
                if (_fileWrapper.DirectoryExists(_appSettings.RoverDateImagesPath))
                    return new DateFolders(_fileWrapper.GetDirectoryNames(_appSettings.RoverDateImagesPath));
            }
            catch
            {
            }

            return new DateFolders();
        }

        /// <summary>
        /// Get the list of all image files downloded for the specified date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<GalleryImages> GetGalleryImages(string date)
        {
            try
            {
                var galleryPath = Path.Combine(_appSettings.RoverDateImagesPath, date);

                if (_fileWrapper.DirectoryExists(galleryPath))
                    return _mapper.Map<List<GalleryImages>>(_fileWrapper.GetFileNames(galleryPath).ToList().Select(filename => $"{date}|{filename}"));

            }
            catch
            {
            }

            return new List<GalleryImages>();
        }


        /// <summary>
        /// Get the byte contenets of the specific image file, identified by the Id. 
        /// </summary>
        /// <param name="id">Is in the format: date|id</param>
        /// <returns></returns>
        public byte[] GetImageFile(string id)
        {
            byte[] fileBytes = null;
            var datePart = id.Split('|')[0];
            var filenamePart = $"{id.Split('|')[1]}_*.JPG";
            var galleryPath = Path.Combine(_appSettings.RoverDateImagesPath, datePart);

            if (filenamePart.Contains("T_"))
                galleryPath = Path.Combine(galleryPath, "thumbnails");

            if (_fileWrapper.ExistsStartWith(galleryPath, filenamePart))
            {
                fileBytes = _fileWrapper.GetStartWith(galleryPath, filenamePart);
            }

            return fileBytes;
        }


        /// <summary>
        /// Load the text file and download all the rover image data from the nasa's site.
        /// </summary>
        /// <param name="authPassKey">already exchanged secure key</param>
        /// <returns></returns>
        public void LoadNewDates(string authPassKey)
        {
            string[] newDates = null;
            if(_appSettings.AuthPassKey.ToLowerInvariant().Trim().Equals(authPassKey.ToLowerInvariant().Trim()))
            {
                //check for "dates.txt"
                string dateFilePath = Path.Combine(_appSettings.RoverDateImagesPath, _appSettings.DatesFileName);
                if (_fileWrapper.DirectoryExists(_appSettings.RoverDateImagesPath))
                    newDates = _fileWrapper.Get(dateFilePath);

                Parallel.ForEach(newDates, newDate =>
                {
                    DateTime date;
                    string[] formats = {"MMMM dd, yyyy","MMM dd, yyyy","M/d/yyyy", "MM/dd/yyyy","M/dd/yyyy hh:mm"};
                    if (DateTime.TryParse(newDate, out date) || DateTime.TryParseExact(newDate, formats, new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out date))
                    {
                        string formattedDate = date.ToString("yyyy-MM-dd");
                        string newDateFolder = Path.Combine(_appSettings.RoverDateImagesPath, formattedDate);
                        string newDateFolderThumbs = Path.Combine(newDateFolder, "thumbnails");
                        if (!_fileWrapper.DirectoryExists(newDateFolder))
                        {
                            _fileWrapper.CreateDirectory(newDateFolder);
                            _fileWrapper.CreateDirectory(newDateFolderThumbs);

                            RoverAPIData dateData = GetRoverAPIData(_appSettings.MarsRoverAPIURL.Replace("{earth_date}", formattedDate));

                            foreach(Photo photo in dateData.photos ?? new List<Photo>())
                            {
                                string imageFile = Path.Combine(newDateFolder, $"{photo.id}_{photo.camera.full_name}.JPG");
                                string thumbsFile = Path.Combine(newDateFolderThumbs, $"{photo.id}T_{photo.camera.full_name}.JPG");
                                if (DownloadImage(photo.img_src, imageFile))
                                {
                                    _fileWrapper.ResizeImageFile(imageFile, thumbsFile, 200, 200);
                                }
                            }
                        }
                    }
                });

                _fileWrapper.Delete(dateFilePath);
            }
        }

        private RoverAPIData GetRoverAPIData(string marsRoverAPIURL)
        {
            RoverAPIData apiData = null;
            var response = webApiService.Get($"{marsRoverAPIURL}").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                apiData = JsonConvert.DeserializeObject<RoverAPIData>(jsonString);
            }

            return apiData;
        }

        private bool DownloadImage(string url, string pathImageFileName)
        {
            try
            {
                var response = webApiService.Get($"{url}").Result;
                if (response.IsSuccessStatusCode)
                {
                    _fileWrapper.SaveFile(pathImageFileName, response.Content.ReadAsByteArrayAsync().Result);
                }
            }
            catch
            {
                return false;
            }

            return true;

        }
    }
}