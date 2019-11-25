import { AuthInterceptor } from './auth/auth.interceptor';
import { MaterialModule } from './material/material.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { UserService } from './shared/services/user.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from './app-routing/app-routing.module';
import { HeaderComponent } from './header/header.component';
import { UserDashboardComponent } from './Dashboard/user-dashboard/user-dashboard.component';
import { AdminDashboardComponent } from './Dashboard/admin-dashboard/admin-dashboard.component';
import { UserCreateComponent } from './User/user-create/user-create.component';
import { UserListComponent } from './User/user-list/user-list.component';
import { ClaimAddComponent } from './Claim/claim-add/claim-add.component';
import { ClaimListComponent } from './Claim/claim-list/claim-list.component';
import { UserLoginComponent } from './User/user-login/user-login.component';
import { AuthGuard } from './auth/auth.guard';
import {FlexLayoutModule} from '@angular/flex-layout';
import { LayoutComponent } from './layout/layout.component';
import { HomeComponent } from './home/home.component';
import { SideHeaderComponent } from './side-header/side-header.component';
import { UserRegisterComponent } from './User/user-register/user-register.component';
import { MatFileUploadModule } from 'angular-material-fileupload';
import { ClaimListPendingComponent } from './Dashboard/admin-dashboard/claim-list-pending/claim-list-pending.component';
import { ClaimListApprovedComponent } from './Dashboard/admin-dashboard/claim-list-approved/claim-list-approved.component';
import { ClaimListDeclinedComponent } from './Dashboard/admin-dashboard/claim-list-declined/claim-list-declined.component';
import { ClaimEditComponent } from './Claim/claim-edit/claim-edit.component';
import { MatDialogModule } from '@angular/material';
import { ClaimDetailAddComponent } from './claim-detail-add/claim-detail-add.component';
import { BarChartDisplayComponent } from './Dashboard/admin-dashboard/bar-chart-display/bar-chart-display.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    UserDashboardComponent,
    AdminDashboardComponent,
    UserCreateComponent,
    UserListComponent,
    ClaimAddComponent,
    ClaimListComponent,
    UserLoginComponent,
    LayoutComponent,
    HomeComponent,
    SideHeaderComponent,
    UserRegisterComponent,
    ClaimListPendingComponent,
    ClaimListApprovedComponent,
    ClaimListDeclinedComponent,
    ClaimEditComponent,
    ClaimDetailAddComponent,
    BarChartDisplayComponent
  ],
  imports: [
    MatDialogModule,
    MatFileUploadModule,
    MaterialModule,
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule
  ],
  providers: [
    UserService,
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true 
    }
  ],
  bootstrap: [AppComponent],
  entryComponents: [ClaimEditComponent]
})
export class AppModule { }
