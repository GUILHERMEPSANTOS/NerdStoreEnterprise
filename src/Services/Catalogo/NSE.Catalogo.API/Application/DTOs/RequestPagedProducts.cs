namespace NSE.Catalogo.API.Application.DTOs
{
    public class RequestPagedProducts
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Query { get; set; }
    }
}