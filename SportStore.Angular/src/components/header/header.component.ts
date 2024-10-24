import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { MatChipOption } from '@angular/material/chips';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, MatIconModule, MatToolbarModule, MatButtonModule, RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})

export class HeaderComponent {

  constructor(public authService:AuthService, public router:Router, private http:HttpClient) {
  }

  color(name:string){
    return this.router.isActive(name, {paths: 'exact', queryParams: 'exact', fragment: 'ignored', matrixParams: 'ignored'}) ? "accent" : "primary"
  }

  get401Error(code:number) { 
    this.http.get("http://localhost:5295/Users/" + code).subscribe(r => console.log(r), e => console.log(e))
  }

}
