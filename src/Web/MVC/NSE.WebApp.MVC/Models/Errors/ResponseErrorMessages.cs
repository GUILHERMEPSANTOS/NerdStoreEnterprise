namespace NSE.WebApp.MVC.Models.Errors
{
    public class ResponseErrorMessages
    {
        public List<string> Message { get; set; }
        public ResponseErrorMessages()
        {
            Message = new List<string>();
        }

    }
}