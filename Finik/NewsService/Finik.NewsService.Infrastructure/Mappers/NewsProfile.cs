using AutoMapper;
using Finik.NewsService.Contracts;
using Finik.NewsService.Models;

namespace Finik.NewsService.Infrastructure.Mappers;

public class NewsProfile : Profile
{
    public NewsProfile()
    {
        CreateMap<News, NewsDto>().ReverseMap();
    }
}
