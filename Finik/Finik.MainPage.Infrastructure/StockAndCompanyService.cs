using System.Text.Json;

namespace Finik.MainPage.Infrastructure
{
    public class StockAndCompanyService
    {
        private readonly String _url;
        public StockAndCompanyService(String url) 
        { 
            _url = url;
        }

        public async Task<T?> Get<T>(String method, int id)
        {
            using var httpClient = new HttpClient();
            var url = $"{_url}/{method}/{id}";
            var resp = await httpClient.GetAsync(url);
            if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                return default(T);
            var json = await resp.Content.ReadAsStringAsync();
            var obj = JsonSerializer.Deserialize<T>(json);
            return obj;
        }
    }
}
