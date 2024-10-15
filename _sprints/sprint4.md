# Sprint 4 Register and Authentication in Angular

# Регистрация пользователя

- в проекте Angular в папке ```src``` создайте папку ```components``` и создайте два компонента ```header``` и ```home```. Перейдите в ```components``` и выполните команду.

```
ng g c header --skip-tests
```

- также создайте ```home```

# Рендер компонентов

- в шаблоне главного компонента ```app``` вставьте новые компоненты

<app-header>
<app-home/>


# Angular Material

- установите пакет

```
ng add @angular/material
```


# Настройка роутера 

В файле ```app.config.ts``` проверьте импорт функциональности для роутера

```ts
import { provideRouter } from '@angular/router';
```

и подключение провайдера в секцию ```providers```:

```ts
 provideRouter(routes),
```

# Роутер

В файле ```app.routes.ts``` пропишите все маршруты для компонентов:

```ts
import { Routes } from '@angular/router';
import { HeaderComponent } from '../components/header/header.component';
import { HomeComponent } from '../components/home/home.component';

export const routes: Routes = [
    { path: 'header', component: HeaderComponent },
    { path: 'home', component: HomeComponent },
    { path: '', component: HomeComponent },
];
```

Теперь в шаблоне главного компонента ```app``` можно подключить роутер:

```html
<app-header/>
<router-outlet/>
```

https://angular-material.dev/articles/angular-material-3


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