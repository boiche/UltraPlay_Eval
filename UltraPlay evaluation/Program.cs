using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UltraPlay_evaluation;
using UltraPlay_evaluation.Data;
using UltraPlay_evaluation.QueueService;
using WorkerService1;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole().AddDebug();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UltraPlay_EvalContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped(x => new MapperConfiguration(x => x.AddProfile(new DataProfile())).CreateMapper());
builder.Services.AddSingleton<IQueueService>(_ =>
{
    if (!int.TryParse(builder.Configuration["QueueCapacity"],
        out int queueCapacity))
    {
        queueCapacity = 100;
    }

    return new QueueService(queueCapacity);
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
