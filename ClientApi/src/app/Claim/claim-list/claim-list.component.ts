import { ClaimEditComponent } from './../claim-edit/claim-edit.component';
import {  ToastrService } from 'ngx-toastr';
import { UserClaim } from './../../shared/models/user-claim.model';
import { Component, OnInit, Inject } from '@angular/core';
import { ClaimService } from 'src/app/shared/services/claim.service';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import{MatDialog, MatDialogRef,MatDialogConfig} from '@angular/material/dialog';

@Component({
  selector: 'app-claim-list',
  templateUrl: './claim-list.component.html',
  styleUrls: ['./claim-list.component.css']
})


export class ClaimListComponent implements OnInit {

  constructor(private location: Location,
    public dialog: MatDialog,private us: ClaimService,private router: Router, private claimService: ClaimService ,private toastr: ToastrService) { }
 
  claimList:  UserClaim[];
  isClaimListEmpty: boolean;

  ngOnInit() {
    this.getClaims(); 
    console.log(this.claimList);
  }
  
  displayedColumns: string[] = ['date', 'reimbursementType', 'approvedValue','requestedValue', 'currency', 'isProcessed', 'uploadedFilePath','actions' ];
  dataSource = this.claimList;

  getClaims(){
    this.us.getUserReimbursementClaims()
      .subscribe((res: UserClaim[]) => {
        this.isClaimListEmpty=res.length==0?false:true;
        this.claimList = res;
        console.log("Cliams coming from backend response\n",res);
      }, err => {
        console.log(err);
      });
  }

  openDialog(element) {

    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = element;
    this.dialog.open(ClaimEditComponent, dialogConfig);
    console.log("BEFOTR output:", element)
    const dialogRef = this.dialog.open(ClaimEditComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
        (data) => { 
          console.log(data)
          if (data){ 
            this.EditClaim(data);
              
          }else{  
          }        
        }, err => {
          console.log("Error ",err);
        });
      //  dialogRef.close();
        
}
  

  EditClaim(claimToEdit){
    this.claimService.editMyClaim(claimToEdit, claimToEdit.ClaimId)
    .subscribe((res: UserClaim[]) => {
      this.toastr.success('Claim Edited');
  //    this.dialogRef.close();
     this.ngOnInit();
   
    }, err => {
      console.log(err);
    });
  }

  DeleteClaim(id: Number){
    this.claimService.deleteMyClaim(id)
    .subscribe((res: UserClaim[]) => {
      console.log("Claim Approved\n");
      this.toastr.success('Claim Deleted');
     this.ngOnInit();
    }, err => {
      console.log(err);
    });
  }


}


