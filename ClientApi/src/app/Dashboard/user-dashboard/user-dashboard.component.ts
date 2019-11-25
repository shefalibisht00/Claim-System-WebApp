import { User } from './../../shared/models/user.model';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent implements OnInit {



  public userDetail: User;
  //userClaims: any;
 
  constructor(private router: Router, private userService: UserService) { }
 
  ngOnInit() {
    this.userService.getUserClaims().subscribe((data: any) => {
      this.userDetail = data;      
      console.log("User Details> ",data);
    });
  }
 


}

