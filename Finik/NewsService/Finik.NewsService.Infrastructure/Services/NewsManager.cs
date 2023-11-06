using AutoMapper;
using Finik.NewsService.Contracts;
using Finik.NewsService.Core.Abstractions.Services;
using Finik.NewsService.DbData;
using Finik.NewsService.Infrastructure.Exceptions;
using Finik.NewsService.Models;

namespace Finik.NewsService.Infrastructure.Services;

public class NewsManager : INewsManager
{
    private readonly IMapper _mapper;
    private readonly INewsDbRepository _dbRepository;
    private readonly INewsPublisher _newsPublisher;

    public NewsManager(IMapper mapper, INewsDbRepository dbRepository, INewsPublisher newsPublisher)
    {
        _mapper = mapper;
        _dbRepository = dbRepository;
        _newsPublisher = newsPublisher;
    }

    public async Task<NewsDto> CreateNews(NewsDto newsDto)
    {
        var newsEntity = _mapper.Map<News>(newsDto);
        var existedNews = await _dbRepository.GetNews(newsDto.Id);
        if (existedNews == null)
        {
            await _dbRepository.CreateNews(newsEntity);
            return _mapper.Map<NewsDto>(newsEntity);
        }
        throw new NewsException("Already exist");
    }

    public async Task DeleteNews(Guid id) => await _dbRepository.DeleteNews(id);

    public async Task<IReadOnlyList<NewsDto>> GetAllNews()
    {
        var allNews = await _dbRepository.GetAllNews();
        return _mapper.Map<IReadOnlyList<NewsDto>>(allNews);
    }

    public async Task<NewsDto?> GetNews(Guid id)
    {
        var newsEntity = await _dbRepository.GetNews(id);
        return _mapper.Map<NewsDto>(newsEntity);
    }

    public async Task Publish(NewsDto news)
    {
        await _newsPublisher.Publish(news);
        news.IsPublished = true;
        news.PublishedAt = DateTime.Now;
        await UpdateNews(news);
    }

    public async Task UpdateNews(NewsDto newsDto)
    {
        var newsEntity = _mapper.Map<News>(newsDto);
        await _dbRepository.UpdateNews(newsEntity);
    }
}
