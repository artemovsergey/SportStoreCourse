import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatTableModule} from '@angular/material/table';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, MatTableModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})

export class HomeComponent {
  title:string = "Добро пожаловать в Home"
}
