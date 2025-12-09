import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {TOKEN_KEY} from '../constants';
import {Auth} from './auth';

@Injectable({
  providedIn: 'root',
})
export class User {

  constructor(private http: HttpClient) {}

  getUserProfile(){
    return this.http.get(environment.apiBaseUrl+'userProfile');
  }
}
