using Microsoft.EntityFrameworkCore;
using Platform.SyncDataServices.Http;
using PlatformService.Data;

var builder = WebApplication.CreateBuilder(args);
//
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

if (builder.Environment.IsProduction())
{
    Console.WriteLine("---> Using SqlServer db");
    builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("PlatformConnectionString")));
}
else
{
    Console.WriteLine("---> Using InMem db");
    builder.Services.AddDbContext<AppDbContext>(option =>
                                            option.UseInMemoryDatabase("InMem"));
}

builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

builder.Configuration.AddJsonFile($"appsettings.Production.json", optional: true);

System.Console.WriteLine($"---> CommandService Endpoint {builder.Configuration["CommandService"]}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

PrepDb.PrepPopulation(app, app.Environment.IsProduction());

app.Run();

