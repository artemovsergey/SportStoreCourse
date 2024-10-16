import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import User from '../../models/user'
import { UsersService } from '../../services/users.service';
import { Observable, timeInterval, timeout } from 'rxjs';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})

export class HomeComponent implements OnInit {
 
  users$: Observable<User[]> = new Observable
  users:any = []

  constructor(private userService:UsersService) { }

  ngOnInit() {
    this.users$ = this.userService.getUsers()
    this.getUsers()
  }

  getUsers(){
    this.userService.getUsers().subscribe(response => this.users = response)
  }

}
