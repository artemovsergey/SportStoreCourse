using SportStore.API.Data;
using SportStore.API.Extensions;
using SportStore.API.Interfaces;
using SportStore.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<SportStoreContext>();
builder.Services.AddCors();
builder.Services.AddAutoMapper(typeof(Program).Assembly);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCustomExceptionHandler();

app.UseCors(option => option.AllowAnyOrigin());


app.UseAuthorization();
app.MapControllers();
app.Run();
