import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-error-server',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './error-server.component.html',
  styleUrl: './error-server.component.scss'
})
export class ErrorServerComponent {

    error:any;

    // мы можем получить доступ к роутеру только через конструктор
    constructor(private router: Router){
      const navigation = this.router.getCurrentNavigation()
      this.error = navigation?.extras?.state?.['error'];
    } 
}
