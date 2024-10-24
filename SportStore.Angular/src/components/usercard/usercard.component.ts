import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatCard, MatCardHeader, MatCardSubtitle, MatCardTitle } from '@angular/material/card';
import User from '../../models/user';

@Component({
  selector: 'app-usercard',
  standalone: true,
  imports: [MatCard, MatCardHeader, MatCardTitle, MatCardSubtitle],
  templateUrl: './usercard.component.html',
  styleUrl: './usercard.component.scss'
})
export class UsercardComponent {


  @Input() user!: User
  @Output() currentUserState: EventEmitter<User> = new EventEmitter()

  giveUserUp($event: User) {
    console.log($event)
  }

}
