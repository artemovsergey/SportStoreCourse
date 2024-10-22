import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import User from '../../models/user';
import { UsersService } from '../../services/users.service';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { UserComponent } from "../user/user.component";
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [CommonModule, MatSelectModule, MatInputModule,MatTableModule, MatCardModule, MatButtonModule, MatListModule, UserComponent, MatFormFieldModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent {

  users$: Observable<User[]> = new Observable

  users: User[] = []
  
  displayedColumns: string[] = ['id','login','name'];
  
  title: string = "Пользователи"
  viewMode: String = "Таблица"

  constructor(private userService:UsersService) { }

  ngOnInit() {
    this.users$ = this.userService.getUsers()
    this.getUsers()
  }

  public getUsers(){
    this.userService.getUsers().subscribe(r => this.users = r)
  }

  showCurrentUser(user:User) {
    console.log(user)
  }

}