import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';
import User from '../../models/user'
import { UsersLocalService } from '../../services/userslocal.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {

  users: User[] = []

  constructor(private http:HttpClient, public usersLocalService:UsersLocalService) {  
  }

  ngOnInit():void {
    //this.users = this.usersLocalService.getLocalUsers();
    this.getUsers();
  }



}
