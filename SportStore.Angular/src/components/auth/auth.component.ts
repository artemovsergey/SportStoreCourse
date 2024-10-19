import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatIcon } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [FormsModule, MatInputModule, MatFormField, MatLabel, MatIcon],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss'
})
export class AuthComponent {

  model:any = {}

  constructor(private authService: AuthService) {
  }

  login(){
    this.authService.login(this.model)
  }

  sign(){
    this.authService.register(this.model).subscribe({next: r => console.log(r), 
                                                     error: e => console.log(e.error)})
  }

  logout(){

  }
  

}
