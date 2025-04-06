//Global Using
global using EF = UangKuAPI.EntityFramework.Models;
global using G = UangKuAPI.EntitySpaces.Generated;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UangKuAPI.BusinessObjects.Base;
using UangKuAPI.BusinessObjects.Interface;
using UangKuAPI.BusinessObjects.Query;

// Add services to the container.
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var builder = WebApplication.CreateBuilder(args);

#region Connection
var conn = builder.Configuration.GetConnectionString("DatabaseConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//EntityFramework
builder.Services.AddDbContext<BaseFramework>(options =>
{
    options.UseMySql(conn, new MariaDbServerVersion("10.6.19-mariadb"))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});

//EntitySpace
BaseSpaces.initES(conn);
#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "UangKu API",
        Version = "v1",
        Description = "An ASP.NET Core Web API for managing UangKu Mobile App",
        TermsOfService = new Uri("http://mi.rsudtarakanjakarta.rs/"),
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("http://mi.rsudtarakanjakarta.rs/")
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("http://mi.rsudtarakanjakarta.rs/")
        }
    });
});
builder.Services.Configure<Parameter>(builder.Configuration.GetSection("Parameter"));

#region Register Interface
builder.Services.AddScoped<IAppParameter, AppParameter>();
builder.Services.AddScoped<IAppStandardReferenceItem, AppStandardReferenceItem>();
builder.Services.AddScoped<IUser, User>();
builder.Services.AddScoped<IUserReport, UserReport>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
