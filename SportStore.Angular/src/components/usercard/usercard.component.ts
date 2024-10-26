import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatCard, MatCardActions, MatCardContent, MatCardHeader, MatCardSubtitle, MatCardTitle } from '@angular/material/card';
import User from '../../models/user';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-usercard',
  standalone: true,
  imports: [MatButtonModule, MatCard, MatCardHeader, MatCardTitle, MatCardSubtitle, MatCardContent, MatCardActions, RouterModule],
  templateUrl: './usercard.component.html',
  styleUrl: './usercard.component.scss'
})
export class UsercardComponent {


  @Input() user!: User

  @Output() currentUserState = new EventEmitter<User>()
  
  giveUserUp(user: User) {
    this.currentUserState.emit(user)
  }

}
