using AutoMapper;
using NET_MVC_LeetSpeak_Text_Generator.Models;
using NET_MVC_LeetSpeak_Text_Generator.Models.DTO;

namespace NET_MVC_LeetSpeak_Text_Generator.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ApiCall, ApiCallDtoCreate>().ReverseMap();
            CreateMap<ApiCall, ApiCallDtoSelect>().ReverseMap();
        }
    }
}
