import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatOptionModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import { AuthService } from '../../services/authservice.service';

@Component({
  selector: 'app-sign',
  standalone: true,
  imports: [CommonModule,FormsModule, MatInputModule,MatButtonModule, MatSelectModule,MatOptionModule],
  templateUrl: './sign.component.html',
  styleUrl: './sign.component.scss'
})

export class SignComponent {

//parametr
@Input() params: boolean = false
@Output() canceledRegister = new EventEmitter();

model:any = {}
roles:any = [1,2,3]

constructor(private auth: AuthService) { }

sign(){
  this.auth.register(this.model).subscribe(r =>console.log(r),
                                           e => console.log(e.error));
  console.log(this.params)
}

cancel(){
  console.log("canceled")
  this.canceledRegister.emit(false )
}

}
