import { Component } from '@angular/core';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {Router, RouterLink} from '@angular/router';
import {Auth} from '../../shared/services/auth';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styles: ``,
})
export class Login {
  form: any;
  isSubmitted: boolean = false;

  constructor(public formBuilder: FormBuilder,
              private service: Auth,
              private router: Router,
              private toastr: ToastrService) {
    this.form = this.formBuilder.group({
      email:['', Validators.required],
      password: ['', Validators.required],
    })
  }

  hasDisplayableError(controlName: string) : Boolean{
    const control = this.form.get(controlName);

    return Boolean(control?.invalid) &&
      (this.isSubmitted || Boolean(control?.touched) || Boolean(control?.dirty));
  }

  onSubmit(){
    this.isSubmitted = true;
    if(this.form.valid){
      this.service.signin(this.form.value).subscribe({
        next: (res:any) => {
          localStorage.setItem('token', res.token);
          this.router.navigateByUrl('/dashboard');
          },
        error: (err:any) => {
          if(err.status === 400){
            this.toastr.error("Incorrect email or password", "Login failed");
          }
          else{
            console.log('Error during login: \n', err);
          }
        },
      })
    }
  }
}
