using API.DAL;
using API.Middlewares;
using API.Models;
using API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

MongoConfigs mongoConfigs = new MongoConfigs
{
    ConnectionString = "mongodb://root:123@localhost:27017/?authSource=admin",
    DatabaseName = "surveys_db",
    SurveysCollectionName = "surveys"
};
builder.Configuration.GetSection("mongo").Bind(mongoConfigs);

builder.Services.AddScoped<MongoDbContext>();
builder.Services.AddScoped<SurveysRepository>();
builder.Services.AddSingleton(mongoConfigs);

builder.Services.AddExceptionHandler<MongoExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
