namespace NSE.WebApp.MVC.Models.Paginacao
{
    public class PagedResult<TPagedClass> : IPagedList where TPagedClass : class
    {
        public string ReferenceAction { get; set; }
        public IEnumerable<TPagedClass> List { get; set; }
        public int TotalResults { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Query { get; set; }
        public double TotalPages => Math.Ceiling((double)TotalResults / PageSize);
    }

    public interface IPagedList
    {
        public string ReferenceAction { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Query { get; set; }
        public int TotalResults { get; set; }
        public double TotalPages { get; }
    }
}