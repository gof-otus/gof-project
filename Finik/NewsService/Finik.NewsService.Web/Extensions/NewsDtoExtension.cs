using Finik.NewsService.Contracts;
using Finik.NewsService.Web.DTO;

namespace Finik.NewsService.Web.Extensions
{
    public static class NewsDtoExtension
    {
        public static NewsDto FromUpdateRequest(this NewsDto newsDto, UpdateNewsRequest updateRequest)
        {
            if (updateRequest.HeadLine != null)
            {
                newsDto.HeadLine = updateRequest.HeadLine;
            }
            if (updateRequest.Body != null)
            {
                newsDto.Body = updateRequest.Body;
            }
            newsDto.CompanyId = updateRequest.Companyid;
            newsDto.StockId = updateRequest.StockId;
            return newsDto;
        }
    }
}
