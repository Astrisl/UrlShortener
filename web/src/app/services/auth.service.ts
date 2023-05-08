import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user.model';
import { Token } from '../models/token.model';

import { environment } from 'src/environment/environment';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private _http: HttpClient,
    private _router: Router
  ) {

  }

  login(user: User): void {
    this._http.post<Token>(environment.apiHost + 'auth/login', user)
      .subscribe({
        next: token => this._handleLoginResponse(token),
        error: resp => console.log(resp)
      });
  }

  signup(user: User): void {
    this._http.post<string>(environment.apiHost + 'auth/signup', user)
      .subscribe({
        error: resp => console.log(resp)
      });
  }

  logout(): void {
    localStorage.removeItem('token');
    this._router.navigate(['auth']);
  }

  private _handleLoginResponse(resp: Token): void {
    localStorage.setItem('token', resp.token);
    this._router.navigate(['urls']);
  }
}
