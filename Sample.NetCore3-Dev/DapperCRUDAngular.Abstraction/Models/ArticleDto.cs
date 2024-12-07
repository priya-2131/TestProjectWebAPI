using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperCRUDAngular.Abstraction.Models
{
    public class ArticleDto
    {
        public int ArticleId { get; set; }
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
        public string ShortUrl { get; set; }
        public string DesFacet { get; set; }
        public string OrgFacet { get; set; }
        public string PerFacet { get; set; }
        public string GeoFacet { get; set; }
        public string MultimediaUrl { get; set; }
        public string MultimediaFormat { get; set; }
        public int MultimediaHeight { get; set; }
        public int MultimediaWidth { get; set; }
        public string MultimediaType { get; set; }
        public string MultimediaSubtype { get; set; }
        public string MultimediaCaption { get; set; }
        public string MultimediaCopyright { get; set; }
    }

}
