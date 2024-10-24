import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { UserComponent } from '../components/user/user.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { UsercardComponent } from '../components/usercard/usercard.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';


@NgModule({
  declarations: [],
  imports: [
    UsercardComponent, UserComponent
  ],

  // Чтобы функционал бы доступен в других компонентах
  exports: [
    CommonModule, MatSelectModule, MatInputModule, MatTableModule, MatCardModule, MatButtonModule, MatListModule, UserComponent, MatFormFieldModule, UsercardComponent, MatProgressSpinnerModule
  ]

})
export class SportStoreModule { }
