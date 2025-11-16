import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  baseUrl = "http://localhost:5120/"

  constructor(private http: HttpClient){}

  createUser(formData: any){
    return this.http.post(this.baseUrl + 'signup', formData)
  }
}
