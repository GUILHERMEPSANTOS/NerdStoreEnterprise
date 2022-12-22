using NSE.WebApp.MVC;
using NSE.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args)
    .UseStartup<Startup>();

