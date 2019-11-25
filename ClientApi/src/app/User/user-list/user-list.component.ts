import { User } from '../../shared/models/user.model';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../../shared/services/user.service';


@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  constructor(private us: UserService) { }
 
  Emp:  User[];
  isProductListEmpty: boolean;

  ngOnInit() {
    this.getUsers(); 
  }

  getUsers(){
    this.us.getAllUsers()
      .subscribe((res: User[]) => {
        this.isProductListEmpty=res.length==0?false:true;
        this.Emp = res;
      }, err => {
        console.log(err);
      });
  }

}
