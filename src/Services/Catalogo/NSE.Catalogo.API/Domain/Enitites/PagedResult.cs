namespace NSE.Catalogo.API.Domain.Enitites
{
    public class PagedResult<TPagedClass> where TPagedClass : class
    {
        public IEnumerable<TPagedClass> List { get; set; }
        public int TotalResults { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Query { get; set; }
    }
}