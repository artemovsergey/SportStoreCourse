# Sprint 3 Register and Authentication

# Наследование: базовая модель и базовый контроллер

Создайте абстрактный класс ```Base```, который будет служить целью базовой модели для всех моделей.

```Csharp
public abstract class Base   
{
    public Guid Id { get; set; }
    public DateTime CreatedAt  { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set;} = DateTime.UtcNow;
}
```

Теперь можно отнаследоваться от модели ```Base```. Например, класс ```User``` будет выглядеть следующим образом:

```Csharp
public class User : Base
{
    // свойства
}
```

Обратите внимание, что ```Id``` вынесен в базовую модель. Также замените тип данных ```Id``` с ```Guid``` на ```int```.





# Entity Framework Core

Для реализации взаимодействия с релеационной базой данных мы будем использовать ORM - ```Entity Framework Core```. Для этого надо установить пакеты:

- пакет ```Microsoft.EntityFrameworkCore```

для миграций
- ```Microsoft.EntityFrameworkCore.Tools```
- ```Microsoft.EntityFrameworkCore.Design```
    
провайдер для базы данных ```PostgreSQL```
- ```Npgsql.EntityFrameworkCore.PostgreSQL```

При установке пакетов надо соблюдать версионность относительно версии фреймворка .net В данном приложении применяется ```net7.0```

Установить пакеты можно несколькими способами. Можно использовать встроенные средства, а можно применить команду

```
dotnet add .\SportStore.API\ package Microsoft.Entity.Framework -v 7.0.0
```


В результате добавления пакетов ```project``` файл проекта ```SportStore.API``` будет выглядеть следующим образом:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="7.0.5" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="7.0.20" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="7.0.20">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="7.0.20">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0" />
  </ItemGroup>

</Project>
```

Далее, создайте папку ```Data``` и добавьте класс ```SportStoreContext```. Это класс будет конфигурировать отображение моделей данных на таблицы в базе данных.

```Csharp
public class SportStoreContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=SportStoreCourse;Username=;Password=");
    }
}
```

В параметрах подключения к базе данных поставьте свой ```UserName``` и ```Password```

# Миграции

**Замечание**. Перед работой с базой данных надо подготовить модели данных. Вместо типа ```Guid``` нам теперь нужно использовать тип ```int``` для свойства ```Id```. Это требование EF. Внесите изменения в те места, где раньше использовался тип ```Guid```.

В рабочей директории создайте первую миграцию.

```dotnet ef migrations add Initial -s SportStore.API -p SportStore.API```

Далее, выполните эту миграцию. То есть EF создаст реальные таблицы в базе данных PostgreSQL на основе классов моделей данных указанных в контексте базы данных ```SportStoreContext```.

```dotnet ef database update -s SportStore.API -p SportStore.API```

**Замечание**: опция ```-s``` - это стартовый проект, опция ```-p``` - это текущий проект. Либо, можно зайти в проект ```SportStore.API``` явно и не прописывать данные параметры. Также при создании и выполнении миграции сборка решения должна происходить успешно.

# Настройка хранения паролей

- добавьте в модель ```User``` новые свойства

```Csharp
    public string Login {get; set;}
    public required byte[] PasswordHash {get; set;}
    public required byte[] PasswordSalt {get; set;}
```

- создайте новую миграцию по именем ```AddLoginAndPasswordToUsers```
- примените миграцию

**Примечание**: существует обратное проектирование - по готовой базе данных восстановить модели данных - скаффолдинг.

# EF Configuration (option)

Настройка каждого атрибута, связей, типов данных в конкретной базе данных

# Создание репозитория для хранения пользователей в базе данных

- создайте в папке ```Repositories``` новый класс ```UserRepository``` ( старый ```UserRepository``` переименуйте в ```UserLocalRepository```), который будет реализовывать интерфейс ```IUserRepository```, но механизм хранения будет использовать базу данных PostgreSQL.

Обратите внимание на конструктор репозитория и зарегистрируйте в DI данный класс.

```Csharp
public class UserRepository : IUserRepository
{
    private readonly SportStoreContext _db;
    public UserRepository(SportStoreContext db)
    {
        _db = db;
    }

    public User CreateUser(User user)
    {
       try
       {
         _db.Add(user);
         _db.SaveChanges();
         return user;
       }
       catch(SqlTypeException ex)
       {
         throw new SqlTypeException($"Ошибка SQL: {ex.Message}");
       }
       catch (Exception ex)
       {
         throw new Exception($"Ошибка: {ex.Message}");
       }
    }

    // another methods interface
}
```

# DTO

Создайте модель для передачи данных ```UserDto```

```Csharp
public class UserDto
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public record UserRecordDto(string Login, string Password);
```
**Замечание**: Эквивалентом и краткой записью для класса со свойствами является ```record```.

# Seed Data - генерация тестовых данных

- установите библиотеку ```Bogus```

- создайте новый контроллер ```SeedController``` в котором реализуйте метод для генерации пользователей

```Csharp
    [HttpGet("generate")]
    public ActionResult SeedUsers(){

        using var hmac = new HMACSHA512();

        Faker<UserRecordDto> _faker = new Faker<UserRecordDto>("en")
            .RuleFor(u => u.Login, f => GenerateLogin(f).Trim())
            .RuleFor(u => u.Password, f => GeneratePassword(f).Trim().Replace(" ",""));


        string GenerateLogin(Faker faker)
        {
            return faker.Random.Word() + faker.Random.Number(3,5);
        }

        string GeneratePassword(Faker faker)
        {
            return faker.Random.Word() + faker.Random.Number(3, 5);
        }

        var users = _faker.Generate(100).Where(u => u.Login.Length > 4 && u.Login.Length <= 10);
        
        List<User> userToDb = new List<User>();

        try
        {
            
            foreach (var user in users)
            {
                var u = new User()
                {
                    Login = user.Login,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)),
                    PasswordSalt = hmac.Key,
                };

                userToDb.Add(u);

            }

            _db.Users.AddRange(userToDb);
            _db.SaveChanges();
        }
        catch(Exception ex)
        {
            Console.WriteLine($"{ex.InnerException.Message}");
        }

        return Ok(userToDb);
    }
```



- EF HasData
В методе ```OnModelCreating``` контекста базы данных можно генерировать тестовые данные, а также применять пользовательские конфигурации.

```Csharp
        protected override void OnModelCreating(ModelBuilder builder){
            
            builder.ApplyConfigurationsFromAssembly(typeof(SportStoreContext).Assembly);

            builder.Entity<User>().HasData(
                new User(){ Id = 1, Name = "user", ...}
            );

        }
```

- Custom extension

```Csharp
    public static class ApiExtension
    {
        
        public static async Task ResetDatabaseAsync(this WebApplication app){
            using var scope = app.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetService<SportStoreContext>();
                if(context != null){
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.EnsureCreatedAsync();
                }
            }
            catch(Exception ex)
            {

            }
        }

    }
```

Вызов в конвейере

```Csharp
...
await app.ResetDatabaseAsync();
app.Run();
```

# SaveChanges

Переопределение метода для обновление базовой модели

```Csharp
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken){

            foreach(var entry in ChangeTracker.Entries<Base>())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
```

# JWT. Реализация сервиса

 - создайте интерфейс

```Csharp
    public interface ITokenService
    {
        string CreateToken(int UserId);
    }
```

- установите пакет ```Microsoft.IdentityModel.Tokens```


- реализуйте сервис для генерации jwt - токена

```Csharp
public class TokenService : ITokenService
{

    private readonly SymmetricSecurityKey _key;
    public TokenService(IConfiguration config)
    {
      _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]!));   
    }

    public string CreateToken(string UserLogin)
    {
       var claims =  new List<Claim>{
         new Claim(JwtRegisteredClaimNames.Name, UserLogin)
       };

       var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

       var tokenDecriptor = new SecurityTokenDescriptor(){
         Subject = new ClaimsIdentity(claims),
         Expires = DateTime.UtcNow.AddDays(7),
         SigningCredentials = creds
       };

       var tokenHandler = new JwtSecurityTokenHandler();
       var token = tokenHandler.CreateToken(tokenDecriptor);
       return tokenHandler.WriteToken(token);

    }
}
```

**Примечание**: значение ```config["TokenKey"]``` мы получаем из конфигурации ```appsettings.json```. Это открытый ключ шифрования. Поэтому добавьте пару ключ-значение в файл ```appsettings.Development.json```:

appsettings.Development.json
```json
"TokenKey": "super key password for jwt token token token token token token token token "
```

- в модель ```User``` добавьте новое свойство ```public string Token {get; set;}```.
- создайте миграцию и примените для обновления базы данных.
- зарегистрируйте в контейнер DI контроллер ```UserController```.
- внедрите объект ```ITokenService``` в конструктор ```UserController```.
- в методе создания пользователя примените метод генерации токена из сервиса к полю ```Token``` у пользователя.


# Middleware

- установите пакет ```Microsoft.AspNetCore.Authentication.JwtBearer```

Program.cs
```Csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]!)),
        };
    });
```

## Настройка Swagger для работы с jwt

```Csharp
builder.Services.AddSwaggerGen(c =>
{
    
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Приложение", Version = "v2024" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Authorization using jwt token. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
```

- зарегистрируйте сервис авторизации ```builder.Services.AddAuthorization();```, а затем подключите его в конвеейр сразу после процесса аутентификации 

```Csharp
app.UseAuthentication();
app.UseAuthorization();
```

- для удобного тестирования jwt создайте отдельный ```TokenController```

```Csharp
[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{
    private readonly IConfiguration _config;
    public TokenController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet]
    public IActionResult GenerateToken()
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, "user") };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:TokenKey"]!));

        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(365)),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
        
        return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
    }
}
```

- чтобы защитить конечную точку надо поставить атрибут [Authorize]. Теперь, чтобы получить доступ по конечной точке надо передать в запросе заголовка ```Authorization``` валидный jwt-токен формата: ```Bearer {token}```. Проверить валидность токена можно на сайте - jwt.io. Тестирование удобно делать в Postman.

```Csharp
    [Authorize]
    [HttpGet]
    public ActionResult GetUser(){
        return Ok(_repo.GetUsers());
    }
```

# Extensions. Методы расширения

- создайте папку ```Extensions```, в которой создайте класс ```JwtServices```

```Csharp
public static class JwtServices
{
    public static IServiceCollection AddJwtServices(this IServiceCollection services, IConfiguration configuration){
        services.AddSwaggerGen(c =>
                        {
                            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Приложение", Version = "v2024" });
                            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                            {
                                Description = "Authorization using jwt token. Example: \"Bearer {token}\"",
                                Name = "Authorization",
                                In = ParameterLocation.Header,
                                Type = SecuritySchemeType.ApiKey
                            });
                            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                            {
                                {
                                    new OpenApiSecurityScheme
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Type = ReferenceType.SecurityScheme,
                                            Id = "Bearer"
                                        }
                                    },
                                    new string[] { }
                                }
                            });
                        });
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
                            options.SaveToken = true;
                            options.RequireHttpsMetadata = false;
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = false,
                                ValidateAudience = false,
                                ValidateLifetime = false,
                                ValidateIssuerSigningKey = true,
                                
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]!)),
                            };
                        });
        services.AddAuthorization();
            
        return services;
    }
}
```

Теперь мы вынесли эту логику в отдельный файл и можем подключить это все одним сервисом.

```Csharp
builder.Services.AddJwtServices(builder.Configuration);
```

**Примечание**: настройте уровень логирования для отслеживания процесса работы с jwt в файле ```appsettings.json```

```Csharp
"Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.AspNetCore.Authentication": "Debug"
    }
  }
```

# Асинхронность. Работа с Task



**Задание 1**: создайте базовый API контроллер.
**Задание 2**: можно создать репозиторий, который будет работать с базой данных SQL Server.
**Задание 3**: протестируйте jwt-аутентификацию c помошью Swagger, Postman, http-request.