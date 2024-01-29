using Finik.MainPage.Core.Models;
using Finik.MainPage.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Finik.MainPage.EFData.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private MainPageDbContext _context;

        public NewsRepository(MainPageDbContext mainPageDbContext)
        {
            _context = mainPageDbContext;
        }

        public async Task<IEnumerable<News>> GetLastNews()
        {
            return await _context.News.OrderByDescending(x => x.CreatedAt).Take(30).ToListAsync();
        }

        public async Task AddNews(News item)
        {
            await _context.News.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNews(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<News?> GetNews(int id)
        {
            var news = await _context.News.FindAsync(id);
            return news;
        }

        public async Task UpdateNews(News item)
        {
            var newsOld = await _context.News.FindAsync(item.Id);
            if (newsOld != null)
            {
                _context.News.Remove(newsOld);
            }
            await _context.News.AddAsync(item);
            await _context.SaveChangesAsync();
        }
    }
}