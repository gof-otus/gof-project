using AutoMapper;
using Finik.AuthService.Contracts;
using Finik.AuthService.Domain.Models;

namespace Finik.AuthService.Services.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {      
        CreateMap<Contracts.Role, Domain.Models.Role>().ConvertUsing(
            source => new Domain.Models.Role() { Id = (int)source, Name = source.ToString() });
        CreateMap<Domain.Models.Role, Contracts.Role>().ConvertUsing(source => Enum.Parse<Contracts.Role>(source.Name));
        CreateMap<UserDto, User>()
            .ForMember(u => u.Role,s => s.MapFrom(x => x.Role))
            .ReverseMap()
            .ForMember(u => u.Role, s => s.MapFrom(u => u.Role));
    }
}
