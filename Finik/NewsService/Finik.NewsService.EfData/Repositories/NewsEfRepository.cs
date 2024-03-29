﻿using Finik.NewsService.DbData;
using Finik.NewsService.Models;
using Microsoft.EntityFrameworkCore;

namespace Finik.Data.Repositories;

public class NewsEfRepository : INewsDbRepository
{
    private readonly FinikDbContext _dbContext;
    public NewsEfRepository(FinikDbContext dbContext)
    {
        _dbContext = dbContext; 
    }
    public async Task<News> CreateNews(News news)
    {
        var newValue = await _dbContext.News.AddAsync(news);
        await _dbContext.SaveChangesAsync();
        return newValue.Entity;
    }

    public async Task DeleteNews(Guid id)
    {
        var newsToDelete = await _dbContext.News.FindAsync(id);
        if (newsToDelete is not null)
        {
            _dbContext.News.Remove(newsToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IReadOnlyList<News>> GetAllNews() => await _dbContext.News.ToListAsync();

    public async Task<News?> GetNews(Guid id) => await _dbContext.News.FindAsync(id);

    public async Task UpdateNews(News news)
    {
        _dbContext.News.Update(news);
        await _dbContext.SaveChangesAsync();
    }
}
