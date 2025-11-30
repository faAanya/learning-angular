import { Component } from '@angular/core';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styles: ``,
})
export class Login {
  form: any;

  constructor(public formBuilder: FormBuilder) {
    this.form = this.formBuilder.group({
      email:['', Validators.required],
      password: ['', Validators.required],
    })
  }
}
