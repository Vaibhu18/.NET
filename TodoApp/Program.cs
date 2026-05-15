using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("URI")));

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
