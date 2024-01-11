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
            .ForMember(entity => entity.Role, s => s.MapFrom(dto => dto.Role))
            .ForMember(entity => entity.Password, s => s.MapFrom(dto => dto.HashedPassword.Hash))
            .ForMember(entity => entity.Salt, s => s.MapFrom(dto => dto.HashedPassword.Salt))
            .ReverseMap()
            .ForMember(u => u.Role, s => s.MapFrom(u => u.Role))
            .ForMember(dto => dto.HashedPassword, s => s.MapFrom(entity => new HashedPassword(entity.Password, entity.Salt)));
    }
}
