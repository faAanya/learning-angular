import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {TOKEN_KEY} from '../constants';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  constructor(private http: HttpClient){}

  createUser(formData: any){
    return this.http.post(environment.apiBaseUrl + 'signup', formData)
  }

  signin(formData: any){
    return this.http.post<string>(environment.apiBaseUrl + 'signin', formData, {
      responseType: 'json' as const,
    })
  }
  isLoggedIn(){
    return this.getToken() != null ? true : false;
  }

  saveToken(token:string){
    localStorage.setItem(TOKEN_KEY, token);
  }

  getToken(){
    return localStorage.getItem(TOKEN_KEY);
  }

  deleteToken(){
    localStorage.removeItem(TOKEN_KEY);
  }

}
