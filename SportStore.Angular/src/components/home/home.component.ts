import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})


export class HomeComponent implements OnInit {

users: any
title: string = "Home"

constructor(private http:HttpClient) { }

ngOnInit(): void {
  this.getUsers()
}

getUsers() {
  this.http.get('http://localhost:5290/User').subscribe({
    next: response => this.users = response,
    error: error => console.log(error)
  })
}

}
