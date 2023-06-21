using Core.Communication;
using NSE.WebApp.MVC.Models.Token;

namespace NSE.WebApp.MVC.Authentication
{
    public class UserLoginResponse
    {
        public string AcessToken { get; set; }
        public string RefreshToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }
}