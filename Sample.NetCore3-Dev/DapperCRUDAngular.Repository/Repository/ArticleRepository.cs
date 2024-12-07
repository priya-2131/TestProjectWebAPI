using Dapper;
using DapperCRUDAngular.Abstraction.Models;
using DapperCRUDAngular.Abstraction.Repository;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DapperCRUDAngular.InfraStructure.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        IConfiguration _appConfig;

        public ArticleRepository(IConfiguration appConfig)
        {
            _appConfig = appConfig;

        }

        protected IDbConnection GetConnection()
        {
                return new SqlConnection(_appConfig.GetConnectionString("AngularCRUDDbConnection"));
        }

        public async Task<int> InsertArticleAsync(ArticleResult article)
        {
            using (var connection = GetConnection())
            {
                var articleId = await connection.QueryFirstOrDefaultAsync<int>(
                    "InsertArticle",
                    new
                    {
                        article.Section,
                        article.Subsection,
                        article.Title,
                        article.Abstract,
                        article.Url,
                        article.Uri,
                        article.Byline,
                        article.ItemType,
                        article.UpdatedDate,
                        article.CreatedDate,
                        article.PublishedDate,
                        article.ShortUrl
                    },
                    commandType: CommandType.StoredProcedure
                );
                return articleId;
            }
        }

        public async Task InsertArticleFacetAsync(int articleId, string desFacet, string orgFacet, string perFacet, string geoFacet)
        {
            using (var connection = GetConnection())
            {
                await connection.ExecuteAsync(
                    "InsertArticleFacet",
                    new { ArticleId = articleId, DesFacet = desFacet, OrgFacet = orgFacet, PerFacet = perFacet, GeoFacet = geoFacet },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task InsertMultimediaAsync(int articleId, MediaResult multimedia)
        {
            using (var connection = GetConnection())
            {
                await connection.ExecuteAsync(
                    "InsertMultimedia",
                    new
                    {
                        ArticleId = articleId,
                        multimedia.Url,
                        multimedia.Format,
                        multimedia.Height,
                        multimedia.Width,
                        multimedia.Type,
                        multimedia.Subtype,
                        multimedia.Caption,
                        multimedia.Copyright
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<IEnumerable<ArticleDto>> GetArticlesWithFacetsAndMultimediaAsync()
        {
            using (var connection = GetConnection())
            {
                // Open the connection if not already open
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                var query = "EXEC GetArticlesWithFacetsAndMultimedia";  // Stored procedure name

                // Execute the query and map the result to the ArticleDto model
                var articles = await connection.QueryAsync<ArticleDto>(
                    query,
                    commandType: CommandType.StoredProcedure
                );

                return articles;
            }
        }
    }
}
