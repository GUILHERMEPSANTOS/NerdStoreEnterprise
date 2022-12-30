using System.Threading.Tasks;
using NSE.WebApp.MVC.Models.Errors;
using NSE.WebApp.MVC.Models.Token;

namespace NSE.WebApp.MVC.Authentication
{
    public class UserLoginResponse
    {
        public string AcessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }
}