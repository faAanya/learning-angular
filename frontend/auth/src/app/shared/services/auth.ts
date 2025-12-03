import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {TOKEN_KEY} from '../constants';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  baseUrl = "http://localhost:5120/"
  constructor(private http: HttpClient){}

  createUser(formData: any){
    return this.http.post(this.baseUrl + 'signup', formData)
  }

  signin(formData: any){
    return this.http.post<string>(this.baseUrl + 'signin', formData, {
      responseType: 'json' as const,
    })
  }
  isLoggedIn(){
    return localStorage.getItem(TOKEN_KEY) != null ? true : false;
  }

  saveToken(token:string){
    localStorage.setItem(TOKEN_KEY, token);
  }
  deleteToken(){
    localStorage.removeItem(TOKEN_KEY);
  }

}
