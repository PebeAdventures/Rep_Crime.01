using Microsoft.EntityFrameworkCore;
using Rep_Crime._01_LawEnforcement.API.Database.Context;
using Rep_Crime._01_LawEnforcement.API.Database.DAL;
using Rep_Crime._01_LawEnforcement.API.Database.DAL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Secrets.json");

builder.Services.AddDbContext<LawEnforcementDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("myConxStr")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped<ILawEnforcementService, LawEnforcementService>();
builder.Services.AddScoped<ILawEnforcementRepository, LawEnforcementRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
