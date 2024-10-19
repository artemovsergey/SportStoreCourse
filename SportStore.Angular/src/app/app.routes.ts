import { Routes } from '@angular/router';
import { HeaderComponent } from '../components/header/header.component';
import { HomeComponent } from '../components/home/home.component';
import { UsersComponent } from '../components/users/users.component';
import { AuthComponent } from '../components/auth/auth.component';

export const routes: Routes = [
    { path: 'header', component: HeaderComponent },
    { path: 'auth', component: AuthComponent },
    { path: 'users', component: UsersComponent },
    { path: 'home', component: HomeComponent },
    { path: '', component: HomeComponent },
];
