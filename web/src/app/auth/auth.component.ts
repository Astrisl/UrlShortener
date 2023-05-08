import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { User } from '../models/user.model';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit{
  authForm!: FormGroup;
  isLoginMode: boolean = true;

  constructor(private _auth: AuthService) {

  }

  ngOnInit(): void {
    this.authForm = new FormGroup({
      'name': new FormControl(null, [
        Validators.nullValidator,
        Validators.required,
      ]),
      'password': new FormControl(null, [
        Validators.nullValidator,
        Validators.required
      ])
    });
  }

  onSubmit(): void {
    const { name, password } = this.authForm.value;
    const user = new User(name, password);

    if(this.isLoginMode) {
      this._auth.login(user);
    } else {
      this._auth.signup(user);
    }
  }
}
