import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent {
//@ViewChild('editForm') editForm: NgForm

  // @ts-ignore
  user: User

  constructor(private authService: AuthService) {
    this.authService.currentUser$.pipe(take(1)).subscribe(u => this.user = u)
  }

  // @HostListener('window:beforeunload',['event']) unloadNotification($event: any){
  //   if(this.editForm.dirty){
  //     $event.returnValue = true;
  //   }
  // }
}
