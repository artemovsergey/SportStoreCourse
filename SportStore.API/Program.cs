using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SportStore.API.Data;
using SportStore.API.Extensions;
using SportStore.API.Interfaces;
using SportStore.API.Repositories;
using SportStore.Application.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<SportStoreContext>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddCors();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddJwtServices(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
//app.UseHttpsRedirection();
app.UseCustomExceptionHandler();

app.UseCors(option => option.AllowAnyOrigin().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
