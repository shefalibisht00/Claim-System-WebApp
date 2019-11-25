import { Component, OnInit } from '@angular/core';
import { UserClaim } from 'src/app/shared/models/user-claim.model';
import { ClaimService } from 'src/app/shared/services/claim.service';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-claim-list-declined',
  templateUrl: './claim-list-declined.component.html',
  styleUrls: ['./claim-list-declined.component.css']
})
export class ClaimListDeclinedComponent implements OnInit {

  constructor(private claimService: ClaimService) {}
  
 
  isClaimListEmpty: boolean;
  
  ngOnInit() {
   
    this.getDeclinedClaims(); 
    console.log(this.dataSource);
  }
   
  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  }

  displayedColumns: string[] = ['date', 'email','reimbursementType', 'requestedValue', 'currency', 'uploadedFilePath'];
  public dataSource  = new MatTableDataSource<UserClaim>();


  getDeclinedClaims(){
    this.claimService.getDeclinedClaims()
      .subscribe((res: UserClaim[]) => {
        this.isClaimListEmpty=res.length==0?false:true;
        this.dataSource.data = res;
        console.log("Claims Declined response\n",res);
      }, err => {
        console.log(err);
      });
  }
  refresh(){
    window.location.reload();
  }

}
