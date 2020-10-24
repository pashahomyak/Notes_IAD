import { Injectable } from '@angular/core';
import {Router, CanActivate, ActivatedRouteSnapshot} from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({providedIn: "root"})

export class AuthGuardService implements CanActivate {
  constructor(public auth: AuthService, public router: Router) {}

  canActivate(next: ActivatedRouteSnapshot): boolean {
  if (!this.auth.isAuthenticated()) {

    if (next.data.page == 'home') {
      this.router.navigate(['']);
    }
    else if (next.data.page == 'profile') {
      this.router.navigate(['authorization']);
    }

    return false;
  }
  return true;
}}
