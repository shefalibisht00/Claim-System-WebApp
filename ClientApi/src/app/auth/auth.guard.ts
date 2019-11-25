import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { UserService } from '../shared/services/user.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private userService: UserService) { }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    if (localStorage.getItem('userToken') != null) {
      const url: string = state.url;
      let roles = next.data["roles"] as string;
      if (roles) {
        console.log("data passed from routing file", roles);
        var match = this.userService.roleMatch(roles);
        if (match) return true;
        else {
          // If normal user tries to access ADMIN panel
          this.router.navigate(['/home']);
          return false;
        }
      }
      else {
        // Return True i.e allow access for any authenticated user
        return true;
      }
    } else {
      // Not authenticated
      this.router.navigate(['/user/login']);
      return false;
    }

  }

  checkLogin(url: string) {
    if (this.userService.isLoggedIn()) {
      return true;
    }

    this.userService.redirectUrl = url;

    this.router.navigate(['/user/login'], {queryParams: { returnUrl: url }} );
  }



}


