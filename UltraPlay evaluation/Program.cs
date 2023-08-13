using Microsoft.EntityFrameworkCore;
using UltraPlay_evaluation.Data;
using WorkerService1;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole().AddDebug();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UltraPlay_EvalContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
    options.EnableSensitiveDataLogging(true);
});
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

app.Run();