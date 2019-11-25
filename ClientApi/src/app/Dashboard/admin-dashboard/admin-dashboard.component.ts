import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  Ispending: boolean = true;
  Isapproved: boolean= false;
  Isdeclined: boolean = false;
  constructor() { }

  ngOnInit() {
     
  }
  pending(){
this.Ispending = true;
this.Isapproved=false;
this.Isdeclined =false;
  }

  approved(){
    this.Ispending = false;
    this.Isapproved= true;
    this.Isdeclined = false;
  }

  declined(){
    this.Ispending = false;
    this.Isdeclined = true;
    this.Isapproved=false;
  }

}
