import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormGroup, FormsModule, FormControl, Validators, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { MatIcon } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButton } from '@angular/material/button';
import { ToastrService } from 'ngx-toastr';
import User from '../../models/user';
import { NonNullChain } from 'typescript';


@Component({
  selector: 'app-sign',
  standalone: true,
  imports: [ReactiveFormsModule, MatButton, MatFormField, CommonModule, MatLabel, FormsModule, MatIcon, MatInputModule],
  templateUrl: './sign.component.html',
  styleUrl: './sign.component.scss'
})
export class SignComponent {
  model:any = {}
  router:Router = new Router()

  signForm!: FormGroup


  constructor(public authService: AuthService, private toast:ToastrService) {
  
    this.signForm = new FormGroup({
      login: new FormControl("", [Validators.required]),
      password: new FormControl("", [Validators.maxLength(8)])
    }, {validators: [this.checkPasswordConfirmValidator]})

  }

  // валидатор для формы

  checkPasswordConfirmValidator : ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    const login = control.get('login');
    const password = control.get('password');

    if(login?.value !== password?.value){
      console.log("логин: ", login!.value)
      console.log("пароль: ", password!.value)
      return { "checkConfirmLogin": "Пароли не совпадают!" }
    }

    return null

  }

  // валидатор для login
  NameValidator(nameRe: RegExp): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const forbidden = nameRe.test(control.value);
      return forbidden ? {forbiddenName: {value: control.value}} : null;
    };
  }

  //validator для password
  passwordLengthValidator = () : ValidatorFn => {
     return (control: AbstractControl): ValidationErrors | null => {
   
      if (control.value === "123" ) {
        return { "login = 123": true };
      }

      return control.value
     }
  }



  sign(){

    console.log(this.signForm.value)

    // this.authService.register(this.model).subscribe({next: r => {console.log(r);
    //                                                  this.router.navigate(["auth"]);
    //                                                  this.toast.success(`Пользователь ${(r as User).login} зарегистрирован`,"Сообщение")},                                                
    //                                                  })
  }

}
