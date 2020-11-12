import {Component} from '@angular/core';
import {ActivatedRouteSnapshot, Router} from "@angular/router";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})

export class NavMenuComponent {
  isAuthorized: boolean = false;

  constructor(public router: Router) {
    //if (window.location.pathname === '/home') {
    if (localStorage.getItem("token")) {
      this.isAuthorized = true;
    }
  }

  toProfile() {
    this.router.navigate(['profile']);
  }

  logOut() {
    localStorage.removeItem("token");
    window.location.reload();

    this.isAuthorized = false;
  }
}
