import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import User from '../../models/user';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [MatCardModule],
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss'
})
export class UserComponent {

  @Input() u:any
  @Output() currentUserState: EventEmitter<User> = new EventEmitter()


  giveUserUp(user: User){
    this.currentUserState.emit(user)
  }

}
