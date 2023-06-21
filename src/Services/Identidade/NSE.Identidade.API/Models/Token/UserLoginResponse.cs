using NSE.Identidade.API.Models.Token;

namespace NSE.Identidade.API.Models
{
    public class UserLoginResponse
    {
        public string AcessToken { get; set; }
        public Guid RefreshToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
    }
}