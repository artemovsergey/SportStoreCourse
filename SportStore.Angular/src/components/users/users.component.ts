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
    return this.http.get("http://localhost:5050/api/users").subscribe(
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
