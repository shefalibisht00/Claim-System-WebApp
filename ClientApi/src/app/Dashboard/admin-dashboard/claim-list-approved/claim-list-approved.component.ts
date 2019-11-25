import { UserClaimDetail } from './../../../shared/models/user-claim-detail.model';
import { Component, OnInit } from '@angular/core';
import { ClaimService } from 'src/app/shared/services/claim.service';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-claim-list-approved',
  templateUrl: './claim-list-approved.component.html',
  styleUrls: ['./claim-list-approved.component.css']
})

export class ClaimListApprovedComponent implements OnInit {

  constructor(private claimService: ClaimService) { }
  

  isClaimListEmpty: boolean;
  
  ngOnInit() {
    this.getApprovedClaims(); 
    console.log(this.dataSource);
  }
   
  displayedColumns: string[] = ['date', 'email','reimbursementType', 'requestedValue', 'currency', 'uploadedFilePath','approvedBy','approvedAmount',
  'internalNotes'];
  public dataSource  = new MatTableDataSource<UserClaimDetail>();


  getApprovedClaims(){
    this.claimService.getApprovedClaims()
      .subscribe((res: UserClaimDetail[]) => {
        this.isClaimListEmpty=res.length==0?false:true;
        this.dataSource.data = res;
        console.log("Claims Approved response\n",res);
 
      }, err => {
        console.log(err);
      });
  }
  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  }

}
