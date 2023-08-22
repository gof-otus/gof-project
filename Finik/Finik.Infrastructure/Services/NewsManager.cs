using AutoMapper;
using Finik.Contracts;
using Finik.Core.Abstractions.Services;
using Finik.DbData;
using Finik.Infrastructure.Exceptions;
using Finik.Models;

namespace Finik.Infrastructure.Services;

public class NewsManager : INewsManager
{
    private readonly IMapper _mapper;
    private readonly INewsDbRepository _dbRepository;
    public NewsManager(IMapper mapper, INewsDbRepository dbRepository)
    {
        _mapper = mapper;
        _dbRepository = dbRepository;
    }

    public async Task<NewsDto> CreateNewsAsync(NewsDto newsDto)
    {
        var newsEntity = _mapper.Map<News>(newsDto);
        var existedNews = await _dbRepository.GetNewsAsync(newsDto.Id);
        if (existedNews == null)
        {
            await _dbRepository.CreateNewsAsync(newsEntity);
            return _mapper.Map<NewsDto>(newsEntity);
        }
        throw new NewsException("Already exist");
    }

    public async Task DeleteNewsAsync(int id) => await _dbRepository.DeleteNewsAsync(id);

    public async Task<IReadOnlyList<NewsDto>> GetAllNewsAsync()
    {
        var allNews = await _dbRepository.GetAllNewsAsync();
        return _mapper.Map<IReadOnlyList<NewsDto>>(allNews);
    }

    public async Task<NewsDto?> GetNewsAsync(int id)
    {
        var newsEntity = await _dbRepository.GetNewsAsync(id);
        return _mapper.Map<NewsDto>(newsEntity);
    }

    public async Task UpdateNewsAsync(NewsDto newsDto)
    {
        var newsEntity = _mapper.Map<News>(newsDto);
        await _dbRepository.UpdateNewsAsync(newsEntity);
    }
}
