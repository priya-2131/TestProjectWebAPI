using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperCRUDAngular.Abstraction.Models
{

    public class ApiResponse
    {
        public string Status { get; set; }
        public string Copyright { get; set; }
        public string Section { get; set; }
        public string LastUpdated { get; set; }
        public int NumResults { get; set; }
        public string ErrorMessage { get; set; }
        public List<ArticleResult> Results { get; set; }
    }

    public class ArticleResult
    {
        public string Section { get; set; }
        public string Subsection { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Url { get; set; }
        public string Uri { get; set; }
        public string Byline { get; set; }
        public string ItemType { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PublishedDate { get; set; }
        public List<string> DesFacet { get; set; }
        public List<string> OrgFacet { get; set; }
        public List<string> PerFacet { get; set; }
        public List<string> GeoFacet { get; set; }
        public List<MediaResult> Multimedia { get; set; }
        public string ShortUrl { get; set; }
    }

    public class MediaResult
    {
        public string Url { get; set; }
        public string Format { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Type { get; set; }
        public string Subtype { get; set; }
        public string Caption { get; set; }
        public string Copyright { get; set; }
    }
}
