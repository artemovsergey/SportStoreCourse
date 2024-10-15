# Sprint 4 Register and Authentication in Angular

# Регистрация пользователя

- в проекте Angular в папке ```src``` создайте папку ```components``` и создайте компоненты ```header``` и ```users```. Перейдите в ```components``` и выполните команды. Так для ```header```:

```
ng g c header --skip-tests
```
- выполните для ```users```


# Провайдер роутера

```ts
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
```

- в коллекцию ```providers``` внесите новый элемент ```provideRouter(routes)```

# Роутинг

app.routes.ts
```ts
import { Routes } from '@angular/router';
import { HeaderComponent } from '../components/header/header.component';
import { HomeComponent } from '../components/home/home.component';
import { UsersComponent } from '../components/users/users.component';

export const routes: Routes = [
    { path: 'header', component: HeaderComponent },
    { path: 'users', component: UsersComponent },
    { path: 'home', component: HomeComponent },
    { path: '', component: HomeComponent },
];
```

# Компонент router-outlen

- в шаблоне ``app`` внесите изменения:

```html
<app-header/>
<router-outlet/>
```

# Angular Material

```
ng add @angular/material
```

в angular.json в разделе ```styles``` подключите предустановленную тему:

```json
"styles": [
        "@angular/material/prebuilt-themes/indigo-pink.css",
        "src/styles.scss"
    ],
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