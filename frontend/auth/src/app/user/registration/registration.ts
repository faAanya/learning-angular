import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { FirstKeyPipe } from '../../shared/pipes/first-key-pipe';
import { Auth } from '../../shared/services/auth';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FirstKeyPipe],
  templateUrl: './registration.html',
  styles: ``,
})
export class Registration {
   form: any;
   isSubmitted: boolean = false;

  constructor(
    public formBuilder: FormBuilder,
    private service: Auth
  ){
    this.form = this.formBuilder.group({
    fullName:['', Validators.required],
    email:['', [
      Validators.required, 
      Validators.email]],
    password:['',[
      Validators.required,
      Validators.minLength(8)]],
    confirmPassword:['']
  }, {validators:this.passwordMatchValidator})
  }

   passwordMatchValidator: ValidatorFn =  (control: AbstractControl) : null =>{

    const password = control.get('password');
    const confirmPassword = control.get('confirmPassword');

    if(password && confirmPassword && password.value != confirmPassword.value){
      confirmPassword?.setErrors({passwordMisMatch: true})
    }
    else{
       confirmPassword?.setErrors(null)
    }

    return null;
   } 

  onSubmit(){
  this.isSubmitted = true;
  if(this.form.valid){
    this.service.createUser(this.form.value)
      .subscribe({
        next:(res:any)=>{
          if(res.succeeded){
            this.form.reset();
            this.isSubmitted = false;
          }
          console.log(res);
        },
        error: err => console.log('error', err)
      });
  }
}

  hasDisplayableError(controlName: string) : Boolean{
    const control = this.form.get(controlName);

    return Boolean(control?.invalid) && (this.isSubmitted || Boolean(control?.touched));
  }
}
