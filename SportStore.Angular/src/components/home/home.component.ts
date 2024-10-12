import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import  User from '../../models/user'
import getLocalUsers from '../../services/users.service'
import {MatTableModule} from '@angular/material/table';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, MatTableModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})


export class HomeComponent implements OnInit {

  users: User[] = []
  title: string = "Home"
  displayedColumns: string[] = ['id', 'login'];

  constructor(private http:HttpClient) { }

  ngOnInit(): void {
    this.getUsers()
    //this.users = getLocalUsers;
  }

  getUsers() {
    this.http.get<User[]>('http://localhost:5290/api/User').subscribe({
      next: response => this.users = response,
      error: error => console.log(error)
    })
  }

}
