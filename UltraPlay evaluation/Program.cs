using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UltraPlay_evaluation;
using UltraPlay_evaluation.Data;
using WorkerService1;
//using Microsoft.AspNetCore.Mvc.New

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
