# Sprint 5 Register and Authentication in Angular

# Регистрация пользователя

- в проекте Angular в папке ```components``` создайте компоненты ```header``` и ```users```. Для этого перейдите в ```components``` и выполните команды. 

Так для ```header```:

```
ng g c header --skip-tests
```

- выполните также для ```users```


# Провайдер роутера

- проверьте конфигурацию для роутера

app.config.ts
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

- в шаблоне ``app`` внесите изменения. Также импортируйте компонент ```header``` в ```app``` компонент.

```html
<app-header/>
<router-outlet/>
```


# Angular Material

- установите пакет

```
ng add @angular/material
```

в ```angular.json``` в разделе ```styles``` подключите предустановленную тему:

```json
"styles": [
        "@angular/material/prebuilt-themes/indigo-pink.css",
        "src/styles.scss"
    ],
```


## Material Icons

- установка пакета для локальной разработки

- ```npm install material-design-icons --save```
- ```npm install material-design-icons-iconfont --save```

- подключение пакета в ```styles.scss```.

```@import '../node_modules/material-design-icons-iconfont/dist/material-design-icons.css'```

# Создание header

- подключите в компоненте ```header``` модули ```MatIconModule```,```MatToolbarModule```,```MatButtonModule```.

```html
<mat-toolbar color="primary">

  <button mat-icon-button >
    <mat-icon>
      home
    </mat-icon>
  </button>

</mat-toolbar>
```

# Список всех пользователей

- добавьте новую кнопку в ```header```, а также примените маршрутизацию к компоненту ```users```. Для этого импортируйте модуль ```RouterLink``` в компонент ```header```.

```html
  <button mat-icon-button [routerLink] = "['/users']">
      <mat-icon>
          people
      </mat-icon>
  </button>
```

- сделайте ссылку на корневую страницу для кнопки с домом.

**Задание**: перенесите функционал вывода списка пользователей из компонента ```home``` в компонент ```users```. В компоненет ```home``` в шаблоне оставьте только приветствие пользователя.

# Создать компонент auth

- в шаблоне компонента ```auth``` создайте форму:

```html
<div class="auth">
    <h1> Авторизация </h1>
    <form (ngSubmit)= "login()">
        <p>
            <input placeholder="Login" name="login" [(ngModel)]="model.login"/>
        </p>
        <p>
            <input placeholder="Password" name="password" [(ngModel)]="model.password"/>
        </p>
       <button type="submit">Отправить</button>
    </form>
</div>
```

в компоненте ```auth``` создайте свойство ```model:any = {}``` и метод:

```ts
  login(){
    console.log(this.model)
  }
```

Также надо импортировать модуль ```FormModule``` для работы директивы ```[(ngModel)]```.

Проверьте работу формы в консоли браузера


# Подключение компонентов Angular Material

- замените тег ```input``` для логина и пароля:

```html
  <mat-form-field>
      <mat-label>Login</mat-label>
      <input matInput name="login" type="text" [(ngModel)] = "model.login" />
  </mat-form-field>
```

```html
    <mat-form-field>
        <mat-label>Password</mat-label>
        <input matInput name="password" type="password" [(ngModel)] ="model.password" />
    </mat-form-field>
```

- замените отображение кнопки

```html
    <button mat-icon-button type="submit">
        <mat-icon>
          login
        </mat-icon>
    </button>
```

- проверьте импорт в компоненте ```auth```: должны быть ```FormsModule, MatInputModule, MatFormField, MatLabel, MatIcon```

**Замечание**: в консоли браузера может быть предупреждение про autocompele для полей ввода. Поставьте атрибут ```autocomplete="off"``` для полей ввода.



# Условное отображение пользователей

```html
  <a *ngIf="auth.currentUser$ | async"  mat-flat-button color="primary" [routerLink]="['/users']">
    Пользователи
  </a>
```

# Кнопка для выхода

```html
  <button *ngIf="auth.currentUser$ | async" type="button" mat-icon-button (click)="logout()">
    <mat-icon>
      logout
    </mat-icon>
  </button>
```

- создайте сервис ```auth.service.ts```


# authService

```ts
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import User from '../models/user';
import { ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl:String = "http://localhost:5050/api/";

  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http:HttpClient) { }

  register(model:any){
    model.roleid = 1
    return this.http.post<User>(this.baseUrl + "Account/register", model).pipe(
      map((response: User) => {
        const user = response;
        if(user){
          console.log("Пользователь сохранен!")
          this.currentUserSource.next(user);
        }
      })
    )
  }

  login(model:any){
    
    return this.http.post<User>(this.baseUrl + "Account/", model).pipe(
      map((response: User) => {
        const user = response;
        if(user){
          localStorage.setItem("user",JSON.stringify(user))
          this.currentUserSource.next(user);
        }
      })
    )
  }

  // setCurrentUser(user: User){
  //   this.currentUserSource.next(user)
  // }

  logout(){
    localStorage.removeItem("user")
    this.currentUserSource.next(null!)
  }
}
```
