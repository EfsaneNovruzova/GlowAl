using System.Threading.RateLimiting;
using GlowAl.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using GlowAl.Persistence;
using GlowAl.Persistence.Services;
using FluentValidation.AspNetCore;
using FluentValidation;
using GlowAl.Application.Validations.UserValidations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddValidatorsFromAssembly(typeof(AppUserRegisterDtoValidator).Assembly);
builder.Services.RegisterService();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GlowAlDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});


var app = builder.Build();
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
