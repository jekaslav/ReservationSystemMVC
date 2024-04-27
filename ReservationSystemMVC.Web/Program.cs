using ReservationSystemMVC.Web;

var builder = WebApplication.CreateBuilder(args);

Startup.ConfigureServices(builder);

var app = builder.Build();

Startup.SetupPipeline(app);

app.Run();

