import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';

// меняет поведение формы
import { FormsModule } from '@angular/forms';

import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';
import {TextFieldModule} from '@angular/cdk/text-field';

import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {AuthService } from '../../services/authservice.service';
import { User } from '../../models/user';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [MatButtonModule,
            MatIconModule,
            MatToolbarModule,
            RouterModule,
            FormsModule,
            TextFieldModule,
            MatFormFieldModule,
            MatInputModule,
            CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})

export class HeaderComponent implements OnInit {

  ngOnInit(): void {
    // this.setCurrentUser()
  }

  constructor(public auth:AuthService){}

  model: any = {}
  // isLogged: boolean = false


  // setCurrentUser(){
  //    const user : User = JSON.parse(localStorage.getItem('user')!);
  //    this.auth.setCurrentUser(user);
  // }



  login(){
    return this.auth.login(this.model)
    .subscribe(r => {console.log(r);} ,
               e => console.log(e.error))
  }

  logout(){
    this.auth.logout();
    // this.isLogged = false;
    console.log("logout")
  }

  // getCurrentUser(){
  //   this.auth.currentUser$.subscribe(user =>  { this.isLogged = !!user})
  // }

}
