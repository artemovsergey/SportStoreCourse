import { Component, HostListener, ViewChild } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import User from '../../models/user';
import { take } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import {MatFormField, MatInputModule, MatLabel } from '@angular/material/input';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { ToastrService } from 'ngx-toastr';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-edit-user',
  standalone: true,
  imports: [CommonModule, FormsModule, MatInputModule, MatFormField, MatLabel, MatIcon, MatButton],
  templateUrl: './edit-user.component.html',
  styleUrl: './edit-user.component.scss'
})
export class EditUserComponent {

  @ViewChild('editForm') editForm!: NgForm

  user!: User
  constructor(private authService: AuthService,
    private toast: ToastrService,
    private userService: UsersService) {
    this.authService.currentUser$.pipe(take(1)).subscribe(u => this.user = u)
  }

  @HostListener('window:beforeunload',['event']) unloadNotification($event: any){
    if(this.editForm.dirty){
      $event.returnValue = true;
    }
  }

  updateUser(){
    this.userService.update(this.user).subscribe(r => console.log(r))
    this.toast.success("Пользователь обновлен!")
    this.editForm.reset()
  }

}
