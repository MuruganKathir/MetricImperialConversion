using AutoMapper;
using Conversion.Core.ApiModels;
using Conversion.DataAccess;

namespace Conversion.API
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ConversionHistory, HistoryResponse>()
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.User.UserName))
                .ReverseMap();

            CreateMap<HistoryRequest, ConversionHistory>()
                .ReverseMap();
        }
    }
}