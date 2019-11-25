import { CustomValidators } from './../../_helpers/custom-validators';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { UserService } from 'src/app/shared/services/user.service';
import { FormGroup, FormControl, Validators, FormBuilder, NgForm } from '@angular/forms';
import { User } from 'src/app/shared/models/user.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {

  @ViewChild('form',{static: false}) myyform;
  
  public ownerForm: FormGroup;
  BankList = ['HSBC Bank', 'ICICI Bank', 'HDFC Bank', 'YES Bank'];
  roles : any;
  user: User;
  constructor(private toastr: ToastrService,private fb: FormBuilder, private userService: UserService, private router: Router) {
     }
// Choose city using select dropdown
changeBank(e) {
  console.log(e.value)
  this.ownerForm['Bank'].setValue(e.target.value, {
    onlySelf: true
  })
}
   ngOnInit(){
    this.CreateUserForm();
   }

  CreateUserForm() {
    this.ownerForm =  this.fb.group({
      FullName: new FormControl('', [Validators.required, Validators.maxLength(255)]),
      Email: new FormControl('', Validators.compose([Validators.email, Validators.required])),

      Password: new FormControl('', Validators.compose([Validators.required,
      CustomValidators.patternValidator(/\d/, {
        hasNumber: true
      }),
      CustomValidators.patternValidator(/[a-zA-Z]/, {
        hasAlphabet: true
      }),
      CustomValidators.patternValidator(/[ !@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/, {
          hasSpecialCharacters: true
        }),
      Validators.minLength(8)
     ])
      ),

      ConfirmPassword : new FormControl('', Validators.compose([Validators.required])),
      PanNumber: new FormControl('', [Validators.required, Validators.pattern(/\b[a-zA-Z0-9]{10}\b/) ]),
      Bank: new FormControl('', [Validators.required]),
      BankAccountNumber: new FormControl('', [Validators.required,  Validators.pattern(/^[1-9]\d{11}$/) ])
    },{
    validator: CustomValidators.passwordMatchValidator
    }
    );

  
  }

  public createUser = (ownerFormValue) => {
    if (this.ownerForm.valid) {
      this.executeOwnerCreation(ownerFormValue);
    }
  }

  private executeOwnerCreation = (form) => {
     this.userService.registerUser(form)
    .subscribe((data: any) => {
      if (data == null){
        this.toastr.error("Email already taken");
        return;
      }
      console.log("aaja"+data);
      if (data.succeeded == true) {
        this.myyform.resetForm();  
        this.toastr.success('User registration successful');
        this.router.navigate(['/user/login']);
      }
      else{
        this.toastr.error(data.Errors[0]);
      }
     
    });

  }
 
  public hasError = (controlName: string, errorName: string) =>{
    return this.ownerForm.controls[controlName].hasError(errorName);
  }
  public onCancel = () => {
    this.ownerForm.reset();
  }
}
