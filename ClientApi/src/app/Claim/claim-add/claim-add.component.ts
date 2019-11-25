import { UserClaim } from './../../shared/models/user-claim.model';
import { ClaimService } from 'src/app/shared/services/claim.service';
import { Component, OnInit,  HostListener, ElementRef, ViewChild, Input, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators, NG_VALUE_ACCESSOR, ControlValueAccessor, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-claim-add',
  templateUrl: './claim-add.component.html',
  styleUrls: ['./claim-add.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: ClaimAddComponent,
      multi: true
    }
  ]
})

export class ClaimAddComponent implements OnInit {

  @ViewChild('form',{static: false}) myform;

 public ReimbursementTypeList = ['Medical', 'Travel', 'Food', 'Entertainment', 'Misc'];
public CurrencyList = ['INR', 'USD', 'EURO'];
  
  public angForm: FormGroup;
  submitted = false;
  
  fileToUpload: File = null;

  constructor(private toastr: ToastrService,private fb: FormBuilder, private claimService: ClaimService, private router: Router) {
  }

  handleFileInput(file: FileList) {
    this.fileToUpload = file.item(0);
    //Show image preview
    var reader = new FileReader();
    // reader.onload = (event:any) => {
    //   this.imageUrl = event.target.result;
    // }
    reader.readAsDataURL(this.fileToUpload);
  }

  ngOnInit() {
   this.createForm();
  }

  onChange: Function;
  private file: File | null = null;

  @HostListener('change', ['$event.target.files']) emitFiles( event: FileList ) {
    const file = event && event.item(0);
    this.onChange(file);
    this.file = file;
  }

 
  createForm() {
    this.angForm = this.fb.group({
      Date: new FormControl('', [Validators.required]),
      ReimbursementType: new FormControl('', [Validators.required]),
      RequestedValue: new FormControl('', [Validators.required]),
      Currency: new FormControl('', [Validators.required]),
      UploadImage: new FormControl('',[Validators.required])
    });
  }

public addClaim  = (Date, ReimbursementType, RequestedValue, Currency) => {
     this.claimService.addClaim(Date, ReimbursementType, RequestedValue, Currency, this.fileToUpload)
    .subscribe((data: any) => {
      console.log("No error \n",data);
      if (data != null) {
        this.myform.resetForm();  
        this.toastr.success('Claim successful');
        this.router.navigate(['/dashboard']);
      }
      else{
        console.log("Got an error \n",data);
        this.toastr.error(data.Errors[0]);
      }   
    });
  }
 
  public hasError = (controlName: string, errorName: string) =>{
    return this.angForm.controls[controlName].hasError(errorName);
  }
  public onCancel = () => {
    this.angForm.reset();
  }
}
