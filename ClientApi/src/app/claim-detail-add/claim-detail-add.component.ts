import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Location } from '@angular/common';
import { UserClaimDetail } from '../shared/models/user-claim-detail.model';
import {ActivatedRoute} from "@angular/router";
import { ClaimService } from '../shared/services/claim.service';

@Component({
  selector: 'app-claim-detail-add',
  templateUrl: './claim-detail-add.component.html',
  styleUrls: ['./claim-detail-add.component.css']
})

export class ClaimDetailAddComponent implements OnInit {

  public approvedClaimId: Number;
  public ownerForm: FormGroup;

  constructor(private location: Location, private claimService: ClaimService, private toastr: ToastrService,private router: Router,
    private route: ActivatedRoute){
      this.route.params.subscribe( params => {
        this.approvedClaimId = +params.id;
        console.log(this.approvedClaimId);
      });
   }

   ngOnInit() {
    this.ownerForm = new FormGroup({
      approvedBy: new FormControl('', [Validators.required, Validators.maxLength(20)]),
      approvedAmount: new FormControl('', [Validators.required]),
      internalNotes: new FormControl('', [Validators.required, Validators.maxLength(100)])
    });
  }
  public hasError = (controlName: string, errorName: string) =>{
    return this.ownerForm.controls[controlName].hasError(errorName);
  }
 
  public onCancel = () => {
    this.location.back();
  }
  public createOwner = (ownerFormValue) => {
    if (this.ownerForm.valid) {
      this.executeOwnerCreation(ownerFormValue);
    }
  }

  
  private executeOwnerCreation = (form) => {
    this.claimService.approvePendingClaims(form, this.approvedClaimId)
   .subscribe((data: any) => {
     if (data) {
       this.toastr.success('Claim Approved Done!');
       this.router.navigate(['/adminPanel']);
     }
     else{
       console.log("lolol \n",data);
       this.toastr.error(data.Errors[0]);
     }
    
   });
  }

 }
