using AutoMapper;
using mars_rover_models.ViewModels;

namespace mars_rover_BL.AutoMapper
{
    public class GalleryImageProfile : Profile
    {
        /// <summary>
        /// Mapping between the image file names (ID_full name) and GalleryImage data type.
        /// </summary>
        public GalleryImageProfile()
        {
            CreateMap<string, GalleryImages>()

                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Split('_', System.StringSplitOptions.None)[0]))
                .ForMember(dest => dest.ThumbnailId, opt => opt.MapFrom(src => $"{src.Split('_', System.StringSplitOptions.None)[0]}T"))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Split('_', System.StringSplitOptions.None)[1]));
        }

    }
}
