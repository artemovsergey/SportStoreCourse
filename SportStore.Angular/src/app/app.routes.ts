import { Routes } from '@angular/router';
import { HeaderComponent } from '../components/header/header.component';
import { HomeComponent } from '../components/home/home.component';
import { UsersComponent } from '../components/users/users.component';
import { AuthComponent } from '../components/auth/auth.component';
import { SignComponent } from '../components/sign/sign.component';
import { UserComponent } from '../components/user/user.component';
import { authGuard } from '../guards/auth.guard';

export const routes: Routes = [
    { path: 'header', component: HeaderComponent },
    { path: 'auth', component: AuthComponent },
    { path: 'sign', component: SignComponent },
    { path: 'users', component: UsersComponent, canActivate:[authGuard] },

    { path: 'users/:id', component: UserComponent },

    { path: 'home', component: HomeComponent },
    { path: '', component: HomeComponent },
    { path: "**", component: HomeComponent, pathMatch: 'full'}
];
