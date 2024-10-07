# Sprint 1 Introduction


## Проект API

Реализовать базовую функциональность API
1. Использование dotnet CLI

- создайте папку SportStore и перейдите в нее.
- посмотрите с помощью команды ```dotnet new list``` список доступных проектов и создайте проект ```webapi``` с именем SportStore.API
- добавьте решение, находясь в папке рабочей дириктории, командой ```dotnet new sln```
- добавьте в решение проекта API - ```dotnet sln add SportStore.API```
- добавьте файл gitignore - ```dotnet new gitignore```

- откройте начальную архитектуру проекта в Visual Code командой ```code .```

В результате вид обозревателя должен получиться такой:

![](sprint1_1.png)

Для проверки работоспособности приложения запустите API - ```dotnet run --project SportStore.API```. У вас по конечной точке http://localhost:5290/weatherforecast должен выводится результат в формате json

![](spring1_2.png)

- добавьте в проект файл readme.md
- настройка Visual Code(exlude obj and bin)

2. Конечная точка контроллера API

- настройка среды разработки (Visual Code, Visual Studio, Rider)
- создание проекта API (Тип архитектуры All-In)

## Разработка модели приложения

### Настройка простой модели User( Id, Name)

Создайте в проекте ```SportStore.API``` папку ```Entities```, в которой создайте класс User

```Csharp
public class User{

    public Guid Id {get; set;}
    public string Name {get ;set;}

}
```

### Entity Framework Core

Установить пакеты:

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore.Design

В результате добавления пакетов project файл SportStore.APi будет выглядеть следующим образом:

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

Создайте папку ```Data``` и добавьте класс ```SportStoreContext```

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

В параметрах подключения к базе данных поставьте свой UserName и Password


# Миграции

В рабочей директории создайте первую миграцию.

```dotnet ef migtations add Initial -s SportStore.APi -p SportStore.API```

Далее, выполните эту миграцию

```dotnet ef update database -s SportStore.APi -p SportStore.API```

Замечание: -s - это стартовый проект, -p - это текущий проект. Либо, можно зайти в проект SportStore.APi и не прописывать данные параметры.

# Создание конечной точки на CRUD

# Postman(Swagger,request.http) для тестирования API
# Работа с Task
# Настройка gitignore
Занесение appsettings.json 


Результат:
- branch:spring1:Introduction
- pullRequest