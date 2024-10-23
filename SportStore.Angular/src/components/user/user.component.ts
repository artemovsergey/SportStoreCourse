import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import User from '../../models/user';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [MatCardModule, CommonModule, MatProgressSpinnerModule],
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss'
})

export class UserComponent implements OnInit {

  @Input() u: any
  @Output() currentUserState: EventEmitter<User> = new EventEmitter()

  id: number
  currentUser: any = {}
  currentUser$: Observable<User> = new Observable()

  constructor(private activateRoute: ActivatedRoute, private http: HttpClient) {
    this.id = activateRoute.snapshot.params["id"];
    console.log("id = " + this.id)
    this.currentUser$ = this.http.get<User>("http://localhost:5295/Users/" + this.id)
  }

  ngOnInit() {
    this.getUserById();
  }

  getUserById() {
    this.http.get<User>("http://localhost:5295/Users/" + this.id).subscribe(r => { this.currentUser = r; console.log(this.currentUser) })
  }

  giveUserUp(user: User) {
    this.currentUserState.emit(user)
    console.log("click")
  }

}
