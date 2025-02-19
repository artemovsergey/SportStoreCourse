using SportStore.API.Data;
using SportStore.API.Extensions;
using SportStore.API.Interfaces;
using SportStore.API.Middleware;
using SportStore.API.Repositories;
using SportStore.API.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUserRepository, UserLocalRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddScoped<SportStoreContext>();
builder.Services.AddCors();
builder.Services.AddAuthorization();
builder.Services.AddJwtServices(builder.Configuration);


var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();
// app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(o => o.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
app.MapControllers();
app.Run();
