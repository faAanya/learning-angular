import { CommonModule } from '@angular/common';
import {Component, OnInit} from '@angular/core';
import { AbstractControl, FormBuilder, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { FirstKeyPipe } from '../../shared/pipes/first-key-pipe';
import { Auth } from '../../shared/services/auth';
import {ToastrService} from 'ngx-toastr';
import {Router, RouterLink} from '@angular/router';
import {ROLES} from '../../shared/constants';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FirstKeyPipe, RouterLink],
  templateUrl: './registration.html',
  styles: ``,
})
export class Registration implements OnInit {
   form: any;
   isSubmitted: boolean = false;
  protected readonly ROLES = ROLES;


  constructor(
    public formBuilder: FormBuilder,
    private service: Auth,
    private toastr: ToastrService,
    private router: Router,
  ){
    this.form = this.formBuilder.group({
    fullName:['', Validators.required],
    email:['', [
      Validators.required,
      Validators.email]],
    password:['',[
      Validators.required,
      Validators.minLength(8)]],
    confirmPassword:[''],
      role:['', Validators.required],
      gender:['', Validators.required],
      dateOfBirth:['', Validators.required],
      age:[{value: '', disabled:true}]
  }, {validators:this.passwordMatchValidator})

    this.form.get('dateOfBirth')?.valueChanges.subscribe((dob:string) => {
      const age = this.calculateAge(dob);
      this.form.get('age')?.setValue(age);
    });
  }

  ngOnInit(): void {
    if(this.service.isLoggedIn()){
      this.router.navigateByUrl('/dashboard');
    }
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
            this.toastr.success("New user created!", "Registration Successful")
          }

        },
        error: err => {
          if(err.error.errors)
          err.error.errors.forEach((x: any)=>{
            switch(x.code){
              case "DuplicateUserName":
                this.toastr.error('UserName already exists!', 'Registration Failed');
                break;
              case "DuplicateEmail":
                this.toastr.error('Email already exists!', 'Registration Failed');
                break;

              default:
                this.toastr.error('Contact developers', 'Registration Failed');
                console.log(x)
                break;
        }
      })
          else
            console.log("errors: ", err)
    }
      });
  }
}

  hasDisplayableError(controlName: string) : Boolean{
    const control = this.form.get(controlName);

    return Boolean(control?.invalid) &&
      (this.isSubmitted || Boolean(control?.touched) || Boolean(control?.dirty));
  }

  private calculateAge(dob: string): number {
    const birthDate = new Date(dob);
    const today = new Date();

    let age = today.getFullYear() - birthDate.getFullYear();
    const monthDiff = today.getMonth() - birthDate.getMonth();

    if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birthDate.getDate())) {
      age--;
    }

    return age;
  }
}
