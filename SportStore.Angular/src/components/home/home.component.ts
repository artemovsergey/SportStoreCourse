import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import User from '../../models/user'
import { UsersService } from '../../services/users.service';
import { Observable, timeInterval, timeout } from 'rxjs';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import IUser from '../../models/user';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, MatTableModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})

export class HomeComponent implements OnInit {
 
  users$: Observable<User[]> = new Observable

  users: User[] = []
  
  displayedColumns: string[] = ['id', 'name'];
  
  title:string = "Пользователи"

  constructor(private userService:UsersService) { }

  ngOnInit() {
    this.users$ = this.userService.getUsers()
    this.getUsers()
  }

  public getUsers(){
    this.userService.getUsers().subscribe(r => this.users = r)
  }

}
