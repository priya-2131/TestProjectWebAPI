using DapperCRUDAngular.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperCRUDAngular.Abstraction.Repository
{
    public interface IArticleRepository
    {
        Task<int> InsertArticleAsync(ArticleResult article);
        Task InsertArticleFacetAsync(int articleId, string desFacet, string orgFacet, string perFacet, string geoFacet);
        Task InsertMultimediaAsync(int articleId, MediaResult multimedia);
        Task<IEnumerable<ArticleDto>> GetArticlesWithFacetsAndMultimediaAsync();
    }
}
