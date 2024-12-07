using DapperCRUDAngular.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperCRUDAngular.Abstraction.Services
{
   public interface IArticleService
    {
        Task<bool> FetchAndSaveArticles(string apiKey);
        Task<IEnumerable<ArticleDto>> GetArticlesWithFacetsAndMultimediaAsync();
    }
}
