import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import User from '../../models/user';
import { UsersService } from '../../services/users.service';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [CommonModule, MatTableModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent {
  users$: Observable<User[]> = new Observable

  users: User[] = []
  
  displayedColumns: string[] = ['id','login','name'];
  
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
