import { ClaimAddComponent } from './../Claim/claim-add/claim-add.component';
import { AuthGuard } from './../auth/auth.guard';
import { UserLoginComponent } from './../User/user-login/user-login.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserCreateComponent } from '../User/user-create/user-create.component';
import { UserListComponent } from '../User/user-list/user-list.component';
import { AppComponent } from '../app.component';
import { UserDashboardComponent } from '../Dashboard/user-dashboard/user-dashboard.component';
import { AdminDashboardComponent } from '../Dashboard/admin-dashboard/admin-dashboard.component';
import { HomeComponent } from '../home/home.component';
import { UserRegisterComponent } from '../User/user-register/user-register.component';
import { ClaimListComponent } from '../Claim/claim-list/claim-list.component';
import { ClaimListPendingComponent } from '../Dashboard/admin-dashboard/claim-list-pending/claim-list-pending.component';
import { ClaimListDeclinedComponent } from '../Dashboard/admin-dashboard/claim-list-declined/claim-list-declined.component';
import { ClaimDetailAddComponent } from '../claim-detail-add/claim-detail-add.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'home',
    redirectTo: ''
  },
  {
    path: 'user/register',
    component: UserRegisterComponent
  },
  {
    path: 'user/login',
    component: UserLoginComponent
  },
  {
    path: 'user/claim',
    component: ClaimListComponent
  },
  {
    path: 'dashboard',
    component: UserDashboardComponent, canActivate:[AuthGuard], data: { roles: ['NonAdmin'] }
  },
  {
    path: 'adminPanel',
    component: AdminDashboardComponent,
    canActivate: [AuthGuard] , data: { roles: ['Admin'] }
  },
  {
    path: 'claim/add',
    component: ClaimAddComponent,
    canActivate: [AuthGuard] , data: { roles: ['NonAdmin'] }
  },
  {
    path: `claim/:id/details/add`,
    component: ClaimDetailAddComponent,
    canActivate: [AuthGuard] , data: { roles: ['Admin'] }
  },
  {
    path: 'claim/pending',
    component: ClaimListPendingComponent,
    canActivate: [AuthGuard] , data: { roles: ['Admin'] }
  },
  {
    path: 'claim/approved',
    component: ClaimListPendingComponent,
    canActivate: [AuthGuard] , data: { roles: ['Admin'] }
  },
  {
    path: 'claim/declined',
    component: ClaimListDeclinedComponent,
    canActivate: [AuthGuard] , data: { roles: ['Admin'] }
  }
  
  ];

@NgModule({
  declarations: [],
  imports:[RouterModule.forRoot(routes, {
    onSameUrlNavigation: 'reload'
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }

