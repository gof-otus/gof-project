using Finik.DbData;
using Finik.Models;
using Microsoft.EntityFrameworkCore;

namespace Finik.Data.Repositories;

public class NewsEfRepository : INewsDbRepository
{
    private readonly DbContext _dbContext;
    public NewsEfRepository(DbContext dbContext)
    {
        _dbContext = dbContext; 
    }
    public async Task<News> CreateNewsAsync(News news)
    {
        var newValue = await _dbContext.Set<News>().AddAsync(news);
        await _dbContext.SaveChangesAsync();
        return newValue.Entity;
    }

    public async Task DeleteNewsAsync(int id)
    {
        var newsToDelete = await _dbContext.Set<News>().Include(n => n.Author).SingleOrDefaultAsync(n => n.NewsId == id);
        if (newsToDelete is not null)
        {
            _dbContext.Set<News>().Remove(newsToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<News>> GetAllNewsAsync() => await _dbContext.Set<News>().Include(n => n.Author).ToListAsync();

    public async Task<News?> GetNewsAsync(int id) => await _dbContext.Set<News>().Include(n => n.Author).SingleOrDefaultAsync(n => n.NewsId == id);


    public async Task UpdateNewsAsync(News news)
    {
        var newsToUpdate = await _dbContext.Set<News>().Include(n => n.Author).SingleOrDefaultAsync(n => n.NewsId == news.NewsId);
        _dbContext.Set<News>().Update(news);
        await _dbContext.SaveChangesAsync();
    }
}
