using System;
using System.Collections.Generic;
using System.Text;

namespace mars_rover_models.ViewModels
{
    public class GalleryImages
    {

        /// <summary>
        /// DateFolder name + Id part of the image file name.
        /// Each file is named as ID_text from the full name param
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// DateFolder name + Id part of the thumbnail image file name.
        /// </summary>
        public string ThumbnailId { get; set; }

        /// <summary>
        /// The discription of the image. The full name part of the file name.
        /// </summary>
        public string Title { get; set; }
    }
}
