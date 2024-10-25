# Angular

- структура компонента
- привязка данных (к атрибуту и свойству, классу, стилю)
- условные кострукции @if, @switch
- циклы @for
    - - $count: количество элементов коллекции
    - - $first: является ли текущий элемент первым в коллекции
    - - $last: является ли текущий элемент последним в коллекции
    - - $even: является ли индекс текущего элемента четным
    - - $odd: является ли индекс текущего элемента нечетным
- директивы (ngStyle, ngClass, атрибутивные и структурные директивы, ngIf и ng-template, ngFor, ngSwitch)

- сервисы
    - - предоставление данных приложению. Сервис может сам хранить данные в памяти, либо для получения данных может обращаться к какому-нибудь источнику данных, например, к серверу.

    - - сервис может представлять канал взаимодействия между отдельными компонентами приложения

    - - сервис может инкапсулировать бизнес-логику, различные вычислительные задачи, задачи по логгированию, которые лучше выносить из компонентов. Тем самым код компонентов будет сосредоточен непосредственно на работе с представлением. Кроме того, тем самым мы также можем решить проблему повторения кода, если нам потребуется выполнить одну и ту же задачу в разных компонентах и классах

- формы



# .NET

```dotnet new sln```
```dotnet sln add (ls -r **/**.csproj)```

```dotnet add ./SportStore.API reference ./SportStore.Application```
```dotnet add ./SportStore.Infrastructure reference ./SportStore.Application```

```dotnet add ./SportStore.Application reference ./SportStore.Domain```

# Presentation
- handing interactions with the outside world
- presenting or displayind data
- translating data
- managing ui and framework-related elements
- manipulating the application layer

# Application
- Executing the application's use cases

# Infrastructure
- Interacting with the persistence solution
- Interacting with other services (web client, message broker)
- Interacting with the underlying machine (system clock, files)
- Identity Concern

# Domain
- defining domain models
- defining domain errors
- executing business logic
- enforcing business rules

# Подходы по работе с ошибками
- Exception
- Result Pattern
- Middleware 


# Debug in VS Code
- >.NET Generate Assets for Build and Debug

.vscode/lauch.json
```json
{
    // Use IntelliSense to learn about possible attributes.
    // Hover to view descriptions of existing attributes.
    // For more information, visit: https://go.microsoft.com/fwlink/?linkid=830387
    "version": "0.2.0",
    "configurations": [
        {
            // Use IntelliSense to find out which attributes exist for C# debugging
            // Use hover for the description of the existing attributes
            // For further information visit https://github.com/dotnet/vscode-csharp/blob/main/debugger-launchjson.md.
            "name": ".NET Core Launch (web)",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build",
            // If you have changed target frameworks, make sure to update the program path.
            "program": "${workspaceFolder}/SportStore.API/bin/Debug/net7.0/SportStore.API.dll",
            "args": [],
            "cwd": "${workspaceFolder}/SportStore.API",
            "stopAtEntry": false,
            // Enable launching a web browser when ASP.NET Core starts. For more information: https://aka.ms/VSCode-CS-LaunchJson-WebBrowser
            "serverReadyAction": {
                "action": "openExternally",
                "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
            },
            "env": {
                "ASPNETCORE_ENVIRONMENT": "Development"
            },
            "sourceFileMap": {
                "/Views": "${workspaceFolder}/Views"
            }
        },
        {
            "name": ".NET Core Attach",
            "type": "coreclr",
            "request": "attach"
        }
    ]
}
```

# Postman 

- Удобная работа с аутентификацией
- Не надо настраивать Swagger для jwt

# .NET
+ есть способ централизованно хранить пакеты всего решения
API controller делает привязку и проверяет на валидацию



# Angular
+ удобно через консоль создавать компоненты и сервисы

# Архитектура
видео 605 по чистой архитектуре

# Testing
701-706 видео

# NSwag
Генерация кода С# и Ts из Swagger

# Alternative controllers - minimal API
- можно протестировать разные механизмы API (контроллеры,minimal,FastAPI)

# Backend
видео  15 про обобщенный механизм поведения для валидаторов
- pattern Domen Event?

.NET

dotnet sln add **/**.csproj

CQS - более строкий подход к операциям чтения и записи ( если метод изменяет состояние, то он должен возвращать void)
На основе use-case по функция делать команды и запросы

Применение результ паттерна - модель ответа
Пакет ErrorOr
Интерфейс IUnitOfWork для объединений нескольких операций базы данных в транзакцию

- free ssl: zerossl.com
- развертывание через сервис doc ...

- можно связать бекенд через домен api.backend.ru

Вопросы:
1. Можно ли подключить Serilog один раз для всех проектов?
2. Попробовать реализовать разные типы аутентификации
3. Как работать с Identity и ролями


Заметки по архитектуре
Domen - расширить класс Base
Application - в интерфейсах сделать обобщенный тип IAsyncRepository, в mediatr использовать AutoMapper


Технологии: .NET, Angular, Kotlin
Инструменты: VSCode, VS, Rider
Развертывание: Docker-compose
CI-CD: GitLab, Github
Test: xUnit

# Sprint 1 Introduction

Реализовать базовую функциональность API и иметь начальное представление о:
1. Использование dotnet CLI
2. Конечная точка контроллера API
3. Entity Framework
4. Структура проекта API
5. Конфигурация и переменные окружения
6. Git

- настройка среды разработки Visual Code, Visual Studio, Rider
- создание проекта API (Тип архитектуры All-In)
- настройка простой модели User( Id, Name)
- EF
- Создание миграции и контекста базы данных
- Создание конечной точки на CRUD
- Postman(Swagger,request.http)
- Работа с Task
- Настройка gitignore

Результат:
- branch:spring1:Introduction
- pullRequest

# Sprint 2 Introduction in Angular

1. Использование Angular CLI
2. Как создать новое приложение Angular
3. Файлы проекта Angular
4. Процесс загрузки Angular
5. Использование клиентской службы Angular HTTP
6. Запуск приложения Angular по HTTPS
7. Как добавлять пакеты с помощью NPM

- подключение HttpClient
- Observable
- Cors
- Вывод данных в компонент Angular
- Bootstrap, Tailwind, Angular Material
- ngx-bootstrap
- ssl (в курсе есть скрипты)



# Sprint 3 Register and Authentication

Implement basic authentication in our app and have an understanding of:
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


# Sprint 4 Register and Authentication in Angular

Реализуйте в приложении функции входа и регистрации, а также понимание:
1. Создание компонентов с помощью Angular CLI
2. Использование шаблонов форм Angular
3. Использование сервисов Angular
4. Понимание Observables
5. Использование структурных директив Angular для условного отображения элементов на странице
6. Связь компонентов от родительского к дочернему
7. Связь компонентов от дочернего компонента к родительскому

- компонент header
- выбор css фреймворка (Bootstrap, Tailwind, Angular Material)
50

# Sprint 5

Implenet routing in our Angular app and have an understanding of:
1. Angular routing
2. Adding a bootstrap theme
3. Using Angular route guards
4. Using a Shared Module ?


# Sprint 6

Implement global error handling in both the API and the Angular application. Also to have an understanding of:
1. API Middleware
2. Angular Interceptors
3. Troubleshooting exceptions

# Sprint 7

Implement further functionality into our API and gain an understanding of:
1. Entity Framework Relationships
2. EF Conventions
3. Seeding data into the Database
4. The repository pattern
5. Using AutoMapper

# Sprint 8

Implement the components that make up the user interface in our client application and gain an understanding of:
1. Using Typescript types
2. Using the Interceptors to send JWT tokens
3. Using bootstrap for styling
4. Basic css tricks to enhance the look
5. Using a 3rd party photo gallery
6. Using Route params

# Sprint 9

Impelent persistence when updating resources in the API and gaining an understanding of
1. Angular Template forms
2. The CanDeactivate Route Guard
3. The @ViewChild decorator
4. Persisting changes to the API
5. Adding loading indicators to the client app
6. Caching data in Angular services

# Sprint 10

Implenets photo upload functionality in the application and gain an understanding of the followind

1. Photo storage options
2. Adding related entities
3. Using a 3rd party API
4. Using the Debugger (again)
5. Updating and deleting resources
6. What to return when creating resources in a REST based API

# Sprint 11

Implement more advanced forms using Reactive Forms in Angular and understand how to
1. Use Reactive Forms
2. Use Angular Validations for inputs
3. Implements custom validators
4. Implement reusable form controls
5. Working the Date inputs

# Sprint 12 

Implement paging, sorting, filtering and gain an understanding of the following
1. How to implement pagination on the API & client
2. Deferred Execution using IQueryable
3. How to implement filtering on the API & client
4. How to implement sorting on the API & client
5. Using Action Filters
6. Adding a TimeAgo pipe
7. Implement caching in the client for paginated resources


# Sprint 13 

Implemets the 'like user' functionality and gain an understanding of the following
1. Many to many relationships
2. Configuring entities in the DbContext


# Sprint 14

Implement the Messaging functionality and gain an understanding of the following
1. More many to many relationss
2. Using query params in Angular
3. Using Route resolvers in Angular

# Sprint 15

Refactor our code to use ASP.NET Identity and gain an understanding of the following
1. Using .NET Identity
2. Role managment
3. Policy based autorisation ```UserManager<T>```, ```SignInManager<T>```,```RoleManager<T>```


# Sprint 16

Implement SignalR into our application and understand how to
1. Use and set up SignalR on both the API and the client
2. Implement online presence
3. Implement live chat between users

# Sprint 17

Implement the Unit of work pattern and gain an understanding of the following
1. The Unit of Work pattern
2. Optimising queries to the DB
3. Adding a confirm dialog service
4. Finishing touches

# Sprint 18

Actually publish the app and gain an understanding of how to:
1. Prepare the app for publiccation
2. What to consider before publishing
3. Switching DBs
4. Serving static content from the API server
5. Publishing to Heroku (or analog)