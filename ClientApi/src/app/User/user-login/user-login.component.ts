import { Component, OnInit } from '@angular/core';
import { UserService } from '../../shared/services/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormGroup, Validators, FormControl, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {
  public ownerForm: FormGroup;
  isLoginError : boolean = false;
  constructor(private fb: FormBuilder, private userService : UserService,private router : Router) { }


  ngOnInit() {
      this.ownerForm =  this.fb.group({
       Email: new FormControl('', Validators.compose([Validators.email, Validators.required])),
       Password: new FormControl('', [Validators.required])
  });
  this.userService.logout();
}

  loginUser = (ownerFormValue) => {
    if (this.ownerForm.valid) {
     // this.executeOwnerCreation(ownerFormValue);
      console.log("valid \n\n"+ this.ownerForm);
      this.SubmitUserForm(ownerFormValue['Email'], ownerFormValue['Password']);
    }
  }

  SubmitUserForm(email,password){
     this.userService.userAuthentication(email,password).subscribe((data : any)=>{
      localStorage.setItem('userToken',data.access_token);
      localStorage.setItem('userRoles',data.role);
     
      console.log("User-ROLE IS : ",localStorage.getItem('userRoles'));
     // this.router.navigate(['/dashboard']);
      var userRoles: string = JSON.parse(localStorage.getItem('userRoles'));
      this.userService.getUserClaims().subscribe((data: any) => {
      
        // // Store Current logged in user Details
        localStorage.setItem('CurrentUserId', data.id);
        localStorage.setItem('CurrentUserName', data.email);
        console.log("User Details> ",data);
      });
      const redirect = this.userService.redirectUrl ? this.userService.redirectUrl : '/adminPanel';
      this.router.navigate([redirect]);

      if (userRoles.toString() === 'Admin'){
        this.router.navigate(['/home']);
      }else{
        this.router.navigate(['/dashboard']);
      }
     
    },
    (err : HttpErrorResponse)=>{
      this.isLoginError = true;
    });
  }

  public hasError = (controlName: string, errorName: string) =>{
    return this.ownerForm.controls[controlName].hasError(errorName);
  }
  public onCancel = () => {
    this.ownerForm.reset();
  }

}
