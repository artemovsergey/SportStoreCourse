import { Routes } from '@angular/router';
import { HeaderComponent } from '../components/header/header.component';
import { HomeComponent } from '../components/home/home.component';
import { UsersComponent } from '../components/users/users.component';
import { AuthComponent } from '../components/auth/auth.component';
import { SignComponent } from '../components/sign/sign.component';
import { UserComponent } from '../components/user/user.component';
import { authGuard } from '../guards/auth.guard';
import { NotFountComponent } from '../components/errors/not-fount/not-fount.component';
import { ErrorServerComponent } from '../components/errors/error-server/error-server.component';

export const routes: Routes = [

    { path: '', component: HomeComponent },

    {
        path:'',
        runGuardsAndResolvers:"always",
        canActivate:[authGuard],
        children:[
            { path: 'header', component: HeaderComponent },

            { path: 'users', component: UsersComponent },
            { path: 'users/:id', component: UserComponent },
        ]
    },

    { path: 'auth', component: AuthComponent },
    { path: 'not-found', component: NotFountComponent },
    { path: 'error-server', component: ErrorServerComponent },
    { path: 'sign', component: SignComponent },
    { path: "**", component: NotFountComponent, pathMatch: 'full'}

];
