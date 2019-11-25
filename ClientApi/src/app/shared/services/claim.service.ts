import { UserClaim } from './../models/user-claim.model';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ClaimService {

  uri = 'http://localhost:59127/api/claim';
  uri2 = 'http://localhost:59127/api/admin/claim';
  uri3 = 'http://localhost:59127/api/claims';

  private headers: HttpHeaders;
  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  }

  getAllClaims() {
    return this.http.get(`${this.uri}`, { headers: this.headers });
  }

  addClaim(Date, ReimbursementType, RequestedValue, Currency, fileToUpload: File) {
    var ClaimOwnerId = localStorage.getItem('CurrentUserId').toString();
    const formData: FormData = new FormData();
    formData.append('Date', Date);
    formData.append('ReimbursementType', ReimbursementType);
    formData.append('RequestedValue', RequestedValue);
    formData.append('Currency', Currency);
    formData.append('ClaimOwnerId', ClaimOwnerId);
    if (fileToUpload != null) {
      formData.append('UploadImage', fileToUpload, fileToUpload.name);
    }
    return this.http.post<any[]>(`${this.uri}` + '/add', formData);
  }


  getUserReimbursementClaims() {
    return this.http.get(`${this.uri3}`, { headers: this.headers });
  }

  // ***************
  editDeclineClaim(id: Number) {
    var reqHeader = new HttpHeaders({ 'Content-Length': '0' });
    return this.http.put(this.uri2 + '/declined/' + id, { headers: reqHeader });
  }

  editApproveClaim(id: Number) {
    var reqHeader = new HttpHeaders({ 'Content-Length': '0' });
    return this.http.put(this.uri2 + '/approved/' + id, { headers: reqHeader });
  }
  // ***************

  approvePendingClaims(ownerFormValue, claimId: Number) {
    let owner = {
      ApprovedBy: ownerFormValue.approvedBy,
      ApprovedAmount: ownerFormValue.approvedAmount,
      ClaimDetailId: claimId,
      InternalNotes: ownerFormValue.internalNotes,
    };
      console.log("in Claim-- ",owner);
  var reqHeader = new HttpHeaders({'No-Auth':'True'});
  return this.http.post(this.uri2 + '/details/add', owner,{headers : reqHeader});
  }

  // *** ADMIN PANEL *** //
  getPendingClaims() {
    return this.http.get(`${this.uri2}`, { headers: this.headers });
  }

  getApprovedClaims() {
    return this.http.get(`${this.uri2}/details`, { headers: this.headers });
  }

  getDeclinedClaims() {
    return this.http.get(`${this.uri2}` + '/declined', { headers: this.headers });
  }
  // *** ADMIN PANEL *** //


  // *** USER CLAIMS PANEL *** //
  editMyClaim(claim, id: Number) {
    var contentHeader = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' });
    console.log("id is " + id + "Claim is id " + claim.ClaimId)
    return this.http.put(`${this.uri}` + '/' + id, claim, { headers: this.headers });
  }

  deleteMyClaim(id: Number) {
    return this.http.delete(`${this.uri}` + '/' + id, { headers: this.headers });
  }

  // *** ADMIN PANEL *** //
}


