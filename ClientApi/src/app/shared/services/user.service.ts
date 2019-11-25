import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Response } from "@angular/http";
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

import { User } from '../models/user.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  
  uri = 'http://localhost:59127';
  private headers: HttpHeaders;
  redirectUrl: string;

  constructor(private router: Router,private http: HttpClient) { 
    this.headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  }
 
  isLoggedIn() {
    if (localStorage.getItem('userToken')) {
      return true;
    }
    return false;
  }
  
  logout() {
    localStorage.removeItem('userToken');
  }

getAllUsers() {
    return this.http.get(`${this.uri}`, { headers: this.headers });
 }
 
 // Register New Users
registerUser(user : User) {
  const obj = {
    FullName: user.FullName,
    Email: user.Email,
    Password: user.Password,
    ConfirmPassword: user.ConfirmPassword,
    PanNumber: user.PanNumber,
    Bank: user.Bank,
    BankAccountNumber: user.BankAccountNumber
  };
  console.log("in Service-- ",obj);
  var reqHeader = new HttpHeaders({'No-Auth':'True'});
  return this.http.post(this.uri + '/api/User/Register', obj,{headers : reqHeader});
 
}

// Signing in 
userAuthentication(userName, password) {
  var data = "username=" + userName + "&password=" + password + "&grant_type=password";
  var reqHeader = new HttpHeaders({ 'Content-Type': 'application/x-www-urlencoded','No-Auth':'True' });
  return this.http.post(this.uri + '/token', data, { headers: reqHeader });
}
getUserClaims(){
  return  this.http.get(this.uri+'/api/GetUserClaims');
 }

 getAllRoles() {
   var reqHeader = new HttpHeaders({ 'No-Auth': 'True' });
   return this.http.get(this.uri + '/api/GetAllRoles', { headers: reqHeader });
 }
 
 public roleMatch(allowedRoles): boolean {
  var isMatch = false;
  var userRole: string = JSON.parse(localStorage.getItem('userRoles'));
  console.log("userRole:",userRole," --- and --- allowedRoles:", allowedRoles);
  if ( userRole.toString() === allowedRoles[0]){
    isMatch = true; console.log("Matched Above ");
      return isMatch;
  }
  console.log("Nontmatched Above");
  return isMatch;

}

// Logout() {
//   localStorage.removeItem('userToken');   
//   this.router.navigate(['/user/login']);
// }

}

