# Sprint 2 Introduction in Angluar

# Создание проекта Angular

```ng new SportStore.Angular```

# Обзор архитектуры приложения Angular

Проверка работоспособности
```ng serve```

# Создание компонента

```ng g c home --skip-tests```

# Настройка HttpClient

Найдите в проекте файл конфигурации главного компонента ```app.config.ts``` и замените в нем код.

app.config.ts
```ts
import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

export const appConfig: ApplicationConfig = {
  providers: [

              provideZoneChangeDetection({ eventCoalescing: true }),
              provideRouter(routes),
              provideHttpClient(
                withInterceptorsFromDi()
              ), provideAnimationsAsync(),

            ]
};
```

# Observable

# Cors. Подключение и конфигурация

Program.cs
```Csharp
builder.Services.AddCors();
...
app.UseCors(o => o.AllowAnyOrigin().AllowAnyHeader());
```

# Установка пакета Angular Material (Bootstrap, Tailwind, ngx-bootstrap)

```ng add @angular/material```

# Настройка https сертификата для localhost (опция)