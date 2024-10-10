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

Обратите внимание, что ```Id``` вынесен в базовую модель.

# Entity Framework Core

Для реализации взаимодействия с релеационной базой данных мы будем использовать ORM - ```Entity Framework Core```. Для этого надо установить пакеты:

- пакет ```Microsoft.EntityFrameworkCore```

для миграций
- ```Microsoft.EntityFrameworkCore.Tools```
- ```Microsoft.EntityFrameworkCore.Design```
    
провайдер для базы данных ```PostgreSQL```
- ```Npgsql.EntityFrameworkCore.PostgreSQL```

При установке пакетов надо соблюдать версионность относительно версии фреймворка .net В данном приложении применяется ```net7.0```

Установить пакеты можно несколькими способами. Можно использовать встроенные средтва, а можно применить команду

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

```dotnet ef database update -s SportStore.APi -p SportStore.API```

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

# Seed Data

- SeedController + Bogus

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

# Middleware

# Extension. Методы расширения

# Асинхронность. Работа с Task

1. How to store passwords in the Database
2. Using inheritance in C# - DRY
3. Using the C# debugger
4. Using Data Transfer Object (DTOs)
5. Validation
6. JSON Web Tokens (JWT)
7. Using services in C#
8. Middleware
9. Extension methods - DRY

- миграция на добавления PasswordHash и PasswordSalt byte[]
- Dto
- Валидация
- Авторизация без jwt и с jwt
- Сервис создания токена
- Authorize Middleware


**Задание 1**: создайте базовый API контроллер.
**Задание 2**: можно создать репозиторий, который будет работать с базой данныз SQL Server.