using System;
using System.Collections.Generic;
using System.Linq;

namespace mars_rover_models.ViewModels
{
    public class DateFolders
    {
        /// <summary>
        /// Directory containing the mars rover images for that date.
        /// </summary>
        public List<string> Names { get; set; }

        public DateFolders(string[] folders)
        {
            Names = folders.ToList();
        }
        public DateFolders()
        {
            Names = new List<string>();
        }
    }
}
