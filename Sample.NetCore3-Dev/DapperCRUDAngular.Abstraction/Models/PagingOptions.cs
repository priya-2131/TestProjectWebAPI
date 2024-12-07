namespace DapperCRUDAngular.Abstraction.Models
{
    public class Pagination
    {
        public Pagination() {

            PageIndex = 1;
            PageSize = 50;
            TotalPages = 0;
            TotalRecords = 0;
        }

        public int PageIndex;
        public int PageSize;
        public int TotalPages;
        public int TotalRecords;
    }
}
