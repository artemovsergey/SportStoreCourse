# Sprint 4 Register and Authentication in Angular

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






```html
  <a *ngIf="auth.currentUser$ | async"  mat-flat-button color="primary" [routerLink]="['/users']">
    Пользователи
  </a>
```

# Модель пользователя в компоненте header

```ts
model: any = {}
```

# Метод login в компоненте header

```ts
  login(){
    return this.auth.login(this.model)
    .subscribe(r => {console.log(r);} ,
               e => console.log(e.error))
  }
```

# Метод выхода в компоненте header

```ts
  logout(){
    this.auth.logout();
    // this.isLogged = false;
    console.log("logout")
  }
```

# Форма авторизации

```html
  <form *ngIf="(auth.currentUser$ | async) === null" (ngSubmit) ="login()" autocomplete="off">

    <mat-form-field>
      <mat-label>Login</mat-label>
      <input matInput name="login" type="text" [(ngModel)] = "model.login" />
    </mat-form-field>

    <mat-form-field>
      <mat-label>Password</mat-label>
      <input matInput name="password" type="password" [(ngModel)] ="model.password" />
    </mat-form-field>

    <button *ngIf="(auth.currentUser$ | async) === null" mat-icon-button type="submit">
      <mat-icon>
        login
      </mat-icon>
    </button>

  </form>
```

# Кнопка для выхода

```html
  <button *ngIf="auth.currentUser$ | async" type="button" mat-icon-button (click)="logout()">
    <mat-icon>
      logout
    </mat-icon>
  </button>
```

# Подключение сервиса в коструктор компонента header

```ts
constructor(public auth:AuthService){}
```

# Стили для компонента header

```scss
mat-form-field {
  margin: 10px;
}

mat-toolbar{
  height: auto;
}
```

# Компоненте home. Шаблон

```html
<h1> {{title}}</h1>

<table mat-table [dataSource]="users" class="mat-elevation-z8">
  <ng-container matColumnDef="id">
    <th mat-header-cell *matHeaderCellDef> Id </th>
    <td mat-cell *matCellDef="let element"> {{element.id}} </td>
  </ng-container>

  <ng-container matColumnDef="login">
    <th mat-header-cell *matHeaderCellDef> Name </th>
    <td mat-cell *matCellDef="let element"> {{element.login}} </td>
  </ng-container>

  <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
  <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
</table>
```

# Компоненте home

```ts
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import  User from '../../models/user'
import getLocalUsers from '../../services/users.service'
import {MatTableModule} from '@angular/material/table';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, MatTableModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})

export class HomeComponent implements OnInit {

  users: User[] = []
  title: string = "Home"
  displayedColumns: string[] = ['id', 'login'];

  constructor(private http:HttpClient) { }

  ngOnInit(): void {
    this.getUsers()
    //this.users = getLocalUsers;
  }

  getUsers() {
    this.http.get<User[]>('http://localhost:5290/api/User').subscribe({
      next: response => this.users = response,
      error: error => console.log(error)
    })
  }

}
```

# Компоненет ```users```. Шаблон

```html
<ul>
  <li *ngFor="let u of users">

    <mat-card class="example-card" appearance="outlined">
      <mat-card-header>
        <div mat-card-avatar class="example-header-image"></div>
        <mat-card-title>{{u.id}}</mat-card-title>
        <mat-card-subtitle>{{u.login}}</mat-card-subtitle>
      </mat-card-header>
      <img mat-card-image src="https://material.angular.io/assets/img/examples/shiba2.jpg" alt="Photo of a Shiba Inu">
      <mat-card-content>
        <p>
            User
        </p>
      </mat-card-content>
      <mat-card-actions>
        <button mat-button>LIKE</button>
        <button mat-button>SHARE</button>
      </mat-card-actions>
    </mat-card>
  </li>
</ul>
```

# Компонент users

```ts
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {MatCardModule} from '@angular/material/card';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [MatCardModule, CommonModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})

export class UsersComponent implements OnInit  {

  users:any;

  constructor(private http:HttpClient){}

  ngOnInit(): void {
     this.getUsers();
  }

  getUsers(){
    return this.http.get("http://localhost:5290/api/user").subscribe(
      response => {this.users = response; console.log(response)},
      error => { console.log(error)}
    )
  }

  getUsersAsync(){
    return this.http.get("http://localhost:5050/api/users").subscribe(r => {r})
  }
}

export interface IUser{
   name: String,
   age: number,
   isLogged: boolean
}
```

# userLocalService

```ts
import User from "../models/user";

function getLocalUsers(): User[] {
    var users = [{"id":"1","name":"user1","login":"login"},
                 {"id":"2","name":"user2","login":"login"},
                 {"id":"3","name":"user2","login":"login"}]
    return users;
}
export default getLocalUsers()
```

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
    model.roleid = 1
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

Реализуйте в приложении функции входа и регистрации, а также понимание:
1. Создание компонентов с помощью Angular CLI
2. Использование шаблонов форм Angular
3. Использование сервисов Angular
4. Понимание Observables

# Использование структурных директив Angular для условного отображения элементов на странице

# Связь компонентов от родительского к дочернему

# Связь компонентов от дочернего компонента к родительскому

- компонент header
- выбор css фреймворка (Bootstrap, Tailwind, Angular Material)

**Ресурсы для изучения**:

https://angular-material.dev/articles/angular-material-3