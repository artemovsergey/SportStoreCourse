import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import User from '../../models/user'
import { UsersService } from '../../services/users.service';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
 
  // users: User[] = []

  users$: Observable<User[]>

  constructor(private userService:UsersService) {   
    this.users$ = userService.getUsers()
  }

  ngOnInit() {
    // this.users$.subscribe(r => console.log(r))
  }


}
