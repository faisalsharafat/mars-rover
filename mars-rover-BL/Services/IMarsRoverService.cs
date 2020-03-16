using System.Collections.Generic;
using mars_rover_models.ViewModels;

namespace mars_rover_BL.Services
{
    public interface IMarsRoverService
    {


        /// <summary>
        /// Get the already downloaded rover image dates as a list.
        /// </summary>
        /// <returns></returns>
        DateFolders GetDateFolders();


        /// <summary>
        /// Get the list of all image files downloded for the specified date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        List<GalleryImages> GetGalleryImages(string date);



        /// <summary>
        /// Get the byte contenets of the specific image file, identified by the Id. 
        /// </summary>
        /// <param name="id">Is in the format: date|id</param>
        /// <returns></returns>
        byte[] GetImageFile(string id);



        /// <summary>
        /// Load the text file and download all the rover image data from the nasa's site.
        /// </summary>
        /// <param name="authPassKey">already exchanged secure key</param>
        /// <returns></returns>
        void LoadNewDates(UploadedFile uploadedFile);
    }
}
