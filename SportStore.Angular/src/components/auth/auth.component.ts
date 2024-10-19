import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatIcon } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [CommonModule, FormsModule, MatInputModule, MatFormField, MatLabel, MatIcon],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss'
})
export class AuthComponent {

  model:any = {}
  constructor(public authService: AuthService) {
  }

  login(){
    console.log("login")
    this.authService.login(this.model).subscribe({next: r => console.log(r), error: e => console.log(e)})
  }

  sign(){
    this.authService.register(this.model).subscribe({next: r => console.log(r), 
                                                     error: e => console.log(e.error)})
  }

  logout(){
    this.authService.logout()
  }
  

}
