import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatIcon } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [CommonModule, FormsModule, MatInputModule, MatFormField, MatLabel, MatIcon, MatButton],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss'
})
export class AuthComponent implements OnInit {

  model:any = {}
  router:Router = new Router()

  

  constructor(public authService: AuthService) {
  }


  ngOnInit() {
    const storedUser = JSON.parse(localStorage.getItem("user")!);
    if (storedUser) {
      this.authService.currentUserSource.next(storedUser);
    }
  }

  login(){
    this.authService.login(this.model).subscribe({next: r => {console.log(r);this.router.navigate([""])  }, error: e => console.log(e)})
  }

  sign(){
    this.authService.register(this.model).subscribe({next: r => console.log(r)})
  }

  logout(){
    this.authService.logout()
  }
  

}
