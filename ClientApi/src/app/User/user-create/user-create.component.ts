import { Component, OnInit } from '@angular/core';
import { User } from '../../shared/models/user.model';
import { ToastrService } from 'ngx-toastr'
import { UserService } from '../../shared/services/user.service';
import {NgForm , FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-create',
  templateUrl: './user-create.component.html',
  styleUrls: ['./user-create.component.css']
})
export class UserCreateComponent implements OnInit {

  roles : any;
  user: User;
  emailPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
  passwordPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
 // passPattern = "^";
 BankList = ['HSBC Bank', 'ICICI Bank',
            'HDFC Bank', 'YES Bank'];

  constructor(private toastr: ToastrService, private userService: UserService, private router: Router) {  }
  
  ngOnInit() {
    this.resetForm();
    this.userService.getAllRoles().subscribe(
      (data : any)=>{
       // data.forEach(obj => obj.selected = false);
       console.log(" ROLSE IS --> ", data);
       
        this.roles = data;
      }
    );
  }

  resetForm(form?: NgForm) {
    if (form != null)
      form.reset();
      this.user = {
        Id: '',
      FullName: '',
      Email: '',
      Password: '',
      ConfirmPassword: '',
      PanNumber: '',
      Bank: '',
      BankAccountNumber: ''
    }
  }
 

  OnSubmit(form: NgForm) {
    console.log(form);
    this.userService.registerUser(form.value)
      .subscribe((data: any) => {
        console.log("dssd \n",data);
        if (data.succeeded == true) {
          this.resetForm(form);
          this.toastr.success('User registration successful');
        }
        else
        console.log("lolol \n",data);
          this.toastr.error(data.Errors[0]);
      });
  }
}

