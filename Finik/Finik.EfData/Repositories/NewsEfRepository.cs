using Finik.DbData;
using Finik.Models;
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

    public async Task DeleteNews(int id)
    {
        var newsToDelete = await _dbContext.News.Include(n => n.Author).SingleOrDefaultAsync(n => n.Id == id);
        if (newsToDelete is not null)
        {
            _dbContext.News.Remove(newsToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IReadOnlyList<News>> GetAllNews() => await _dbContext.News.Include(n => n.Author).ToListAsync();

    public async Task<News?> GetNews(int id) => await _dbContext.News.Include(n => n.Author).SingleOrDefaultAsync(n => n.Id == id);

    public async Task UpdateNews(News news)
    {
        if (await _dbContext.News.Include(n => n.Author).SingleOrDefaultAsync(n => n.Id == news.Id) is null) 
            throw new ArgumentException($"New with id {news.Id} doesn't exits");
        _dbContext.News.Update(news);
        await _dbContext.SaveChangesAsync();
    }
}
