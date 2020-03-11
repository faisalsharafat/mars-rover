using System.Collections.Generic;
using mars_rover_models.ViewModels;

namespace mars_rover_BL.Services
{
    public interface IMarsRoverService
    {
        DateFolders GetDateFolders();

        List<GalleryImages> GetGalleryImages(string date);

        byte[] GetImageFile(string id);

        void LoadNewDates(string authPassKey);
    }
}
