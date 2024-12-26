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
        private readonly ApiSettings _apiSettings;
        public ArticleService(HttpClient httpClient, IArticleRepository articleRepository, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _articleRepository = articleRepository;
            _apiSettings = apiSettings.Value;
        }
        public async Task<ApiResponse> FetchAndSaveArticles(string apikey)
        {
            var apiResponse = new ApiResponse();
            var configuredApiKey = _apiSettings.ApiKey;          
            var url = _apiSettings.ApiUrl;
            //if (configuredApiKey != apikey)
            //{
            //    return new ApiResponse
            //    {
            //        ErrorMessage = $"API key is not valid"
            //    };
            //}

            try
            {
                var fullUrl = $"{url}?api-key={apikey}";
                var response = await _httpClient.GetAsync(fullUrl);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return new ApiResponse
                        {
                            ErrorMessage = $"API key is not valid."
                        };

                    }
                    else
                    {
                        return new ApiResponse
                        {
                            ErrorMessage = $"Failed to fetch articles. Status code: {response.StatusCode}."
                        };
                    }
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

                // Validate and save articles only if the results are not null
                
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    ErrorMessage = $"An error occurred while fetching articles: {ex.Message}"
                };
            }

            return apiResponse;
        }

        public async Task<IEnumerable<ArticleDto>> GetArticlesWithFacetsAndMultimediaAsync()
        {
            return await _articleRepository.GetArticlesWithFacetsAndMultimediaAsync();
        }
    }
}
