import { Routes } from '@angular/router';
import { HeaderComponent } from '../components/header/header.component';
import { HomeComponent } from '../components/home/home.component';
import { UsersComponent } from '../components/users/users.component';
import { AuthComponent } from '../components/auth/auth.component';
import { SignComponent } from '../components/sign/sign.component';

export const routes: Routes = [
    { path: 'header', component: HeaderComponent },
    { path: 'auth', component: AuthComponent },
    { path: 'sign', component: SignComponent },
    { path: 'users', component: UsersComponent },
    { path: 'users/:id', component: UsersComponent },
    { path: 'home', component: HomeComponent },
    { path: '', component: HomeComponent },
    { path: "**", component: HomeComponent, pathMatch: 'full'}
];
