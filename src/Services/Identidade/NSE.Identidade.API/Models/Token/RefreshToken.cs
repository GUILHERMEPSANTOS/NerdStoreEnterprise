namespace NSE.Identidade.API.Models.Token
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public Guid Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string UserName { get; set; }
        public RefreshToken()
        {
            Token = Guid.NewGuid();
            Id = Guid.NewGuid();
        }
    }
}