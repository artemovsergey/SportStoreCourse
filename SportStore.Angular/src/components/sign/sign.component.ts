import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatIcon } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButton } from '@angular/material/button';
import { ToastrService } from 'ngx-toastr';
import User from '../../models/user';


@Component({
  selector: 'app-sign',
  standalone: true,
  imports: [MatButton, MatFormField, CommonModule, MatLabel, FormsModule, MatIcon, MatInputModule],
  templateUrl: './sign.component.html',
  styleUrl: './sign.component.scss'
})
export class SignComponent {
  model:any = {}
  router:Router = new Router()

  constructor(public authService: AuthService, private toast:ToastrService) {
  }

  sign(){
    this.authService.register(this.model).subscribe({next: r => {console.log(r);
                                                     this.router.navigate(["auth"]);
                                                     this.toast.success(`Пользователь ${(r as User).login} зарегистрирован`,"Сообщение")},                                                
                                                     })
  }

}
