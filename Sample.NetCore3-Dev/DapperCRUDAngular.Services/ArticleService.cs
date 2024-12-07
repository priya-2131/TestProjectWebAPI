using DapperCRUDAngular.Abstraction.Models;
using DapperCRUDAngular.Abstraction.Repository;
using DapperCRUDAngular.Abstraction.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DapperCRUDAngular.Services
{
    public class ArticleService : IArticleService
    {

        private readonly HttpClient _httpClient;
        private readonly IArticleRepository _articleRepository;
        public ArticleService(HttpClient httpClient, IArticleRepository articleRepository, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _articleRepository = articleRepository;
        }
        public async Task<bool> FetchAndSaveArticles(string apiKey)
        {
            var url = $"https://api.nytimes.com/svc/topstories/v2/home.json?api-key={apiKey}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

                if (apiResponse?.Results != null)
                {
                    foreach (var article in apiResponse.Results)
                    {
                        // Insert Article
                        var articleId = await _articleRepository.InsertArticleAsync(article);

                        // Insert Article Facets
                        foreach (var desFacet in article.DesFacet)
                        {
                            await _articleRepository.InsertArticleFacetAsync(articleId, desFacet,
                                string.Join(",", article.OrgFacet),
                                string.Join(",", article.PerFacet),
                                string.Join(",", article.GeoFacet));
                        }

                        // Insert Multimedia
                        foreach (var multimedia in article.Multimedia)
                        {
                            await _articleRepository.InsertMultimediaAsync(articleId, multimedia);
                        }
                    }
                }
            }
            return true;

        }
        public async Task<IEnumerable<ArticleDto>> GetArticlesWithFacetsAndMultimediaAsync()
        {
            return await _articleRepository.GetArticlesWithFacetsAndMultimediaAsync();
        }
    }
}
