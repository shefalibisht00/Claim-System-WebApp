import { UserClaim } from './../../shared/models/user-claim.model';
import { Component, OnInit, VERSION, ViewChild, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormControl, FormGroup, Validators, FormControlDirective } from '@angular/forms';

@Component({
  selector: 'app-claim-edit',
  templateUrl: './claim-edit.component.html',
  styleUrls: ['./claim-edit.component.css']
})
export class ClaimEditComponent implements OnInit {

  form: FormGroup;
  editClaimList : UserClaim;
  claimId : Number;
  currency : string;
  date : Date;
  reimbursementType : string;
  requestedValue : string;
  uploadImage : string;
  claimOwnerId : string;
  approvedValue : string;
  public ReimbursementTypeList = ['Medical', 'Travel', 'Food', 'Entertainment', 'Misc'];
  public CurrencyList = ['INR', 'USD', 'EURO'];
  
  constructor( private fb: FormBuilder, private dialogRef: MatDialogRef<ClaimEditComponent>,
    @Inject(MAT_DIALOG_DATA) data) {
      this.claimId= data.claimId,
      this.date = data.date;
      this.currency = data.currency;
      this.reimbursementType = data.reimbursementType;
      this.requestedValue = data.requestedValue;
     }

  ngOnInit() {
    this.form = this.fb.group({
        ClaimId: new FormControl(this.claimId, []),
        Date: new FormControl(this.date, [Validators.required]),
        ReimbursementType: new FormControl(this.reimbursementType, [Validators.required]),
        RequestedValue: new FormControl(this.requestedValue, [Validators.required]),
        Currency: new FormControl(this.currency, [Validators.required]),
  
      });
    }

 
  save() {
    this.dialogRef.close(this.form.value);
   // this.dialogRef.close();
}

close() {
    this.dialogRef.close();
}

}
