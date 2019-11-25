import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../shared/services/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})


export class HeaderComponent implements OnInit {

  //public Name: string = localStorage.getItem('CurrentUserName').toString();

  // public isLogged: boolean;
  @Output() public sidenavToggle = new EventEmitter();

  constructor(private toastr: ToastrService, private router: Router, private userService: UserService) { 
  }

  ngOnInit() {
     
  }

 get userLoggedRole() {
   var role =  localStorage.getItem('userRoles').toString();
    return  role.substring(2,role.length-2);
  }
  get Name() { return localStorage.getItem('CurrentUserName') }
  get isLoggedIn() { return this.userService.isLoggedIn(); }

  public onToggleSidenav = () => {
    this.sidenavToggle.emit();
  }
}
