using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Rep_Crime._01_LawEnforcement.API.Database.Context;
using Rep_Crime._01_LawEnforcement.API.Database.DAL;
using Rep_Crime._01_LawEnforcement.API.Database.DAL.Interfaces;
using Rep_Crime._01_LawEnforcement.API.Services;
using Rep_Crime._01_LawEnforcement.API.Services.Interface;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Secrets.json");


var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.AddSerilog(logger);

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var connectionString = $"Server = {dbHost}; Database = {dbName}; User Id = sa; Password = {dbPassword};";
builder.Services.AddDbContext<LawEnforcementDbContext>(option =>
    option.UseSqlServer(connectionString));


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ILawEnforcementService, LawEnforcementService>();
builder.Services.AddScoped<ILawEnforcementRepository, LawEnforcementRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<LawEnforcementDbContext>();
    context.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
