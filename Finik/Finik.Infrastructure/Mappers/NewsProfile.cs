using AutoMapper;
using Finik.Contracts;
using Finik.Models;

namespace Finik.Infrastructure.Mappers;

public class NewsProfile : Profile
{
    public NewsProfile()
    {
        CreateMap<News, NewsDto>().ReverseMap();
    }
}
