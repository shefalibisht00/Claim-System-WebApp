import { environment } from './../../../../environments/environment';
import { UserClaim } from './../../../shared/models/user-claim.model';
import { Component, OnInit } from '@angular/core';
import { ClaimService } from 'src/app/shared/services/claim.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material';
import { FormControl, Validators, FormBuilder } from '@angular/forms';
import {DomSanitizer} from '@angular/platform-browser';

@Component({
  selector: 'app-claim-list-pending',
  templateUrl: './claim-list-pending.component.html',
  styleUrls: ['./claim-list-pending.component.css']
})  

export class ClaimListPendingComponent implements OnInit {

 
  areas: string[] = ['Food','Travel','Medical','Entertainment'];
  envUrl = environment.url;

  selectedAreas: string[] = this.areas;
  area = new FormControl('',[
    Validators.required,
  ]);

  myForm = this.builder.group({
    area: this.area
  });
  search(query: string){
    console.log('query', query)
    let result = this.select(query)
    this.selectedAreas = result;
  }

  select(query: string):string[]{
    let result: string[] = [];
    for(let a of this.areas){
      if(a.toLowerCase().indexOf(query) > -1){
        result.push(a)
      }
    }
    return result
  }


  displayedColumns: string[] = ['date', 'email','reimbursementType', 'requestedValue', 'currency', 'uploadedFilePath', 'actions' ];
  public dataSource  = new MatTableDataSource<UserClaim>();

  constructor(public domSanitizer: DomSanitizer,private toastr: ToastrService,private claimService: ClaimService,
     private router: Router,private builder: FormBuilder) { }
  
  isClaimListEmpty: boolean;
 
  submitFilter(sel){
    console.log(sel + "-----------------" );
  }

  ngOnInit(): void { 
    this.getPendingClaims(); 
    console.log(this.dataSource);
  }


  getPendingClaims(){
    this.claimService.getPendingClaims()
      .subscribe((res: UserClaim[]) => {
        this.isClaimListEmpty=res.length==0?false:true;
        this.dataSource.data = res;
        console.log("Claims response\n",res);
      }, err => {
        console.log(err);
      });
  }

  approveClaim(id: Number){
    this.router.navigateByUrl(`/claim/${id}/details/add`);
  }


  declineClaimRequest(id: Number){
    this.claimService.editDeclineClaim(id)
    .subscribe((res: UserClaim[]) => {
      console.log("Claims Declines\n");
      this.toastr.success('Claim Declined');
      this.ngOnInit();
      this.router.navigate(['adminPanel']);
    
    }, err => {
      console.log(err);
    });
  }

  public doFilter = (value: string) => {
      this.dataSource.filter = value.trim().toLocaleLowerCase();
    }
}
