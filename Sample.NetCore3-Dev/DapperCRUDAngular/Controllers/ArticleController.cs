using DapperCRUDAngular.Abstraction.Models;
using DapperCRUDAngular.Abstraction.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperCRUDAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost("SubmitArticle")]
        public async Task<ActionResult<ApiResponse>> FetchAndSaveArticles(string apiKey)
        {
            ApiResponse articleResponse = await _articleService.FetchAndSaveArticles(apiKey);
            return StatusCode(200, articleResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            var articles = await _articleService.GetArticlesWithFacetsAndMultimediaAsync();
            return Ok(articles); // Return the articles as a JSON response
        }
    }
}
