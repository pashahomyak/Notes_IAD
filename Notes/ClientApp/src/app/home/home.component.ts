import {Component, OnInit} from '@angular/core';
import {trigger} from "@angular/animations";
import {Router} from "@angular/router";
import {Local} from "protractor/built/driverProviders";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{
  authorizationIsOpen: boolean = false;
  registrationIsOpen: boolean = false;
  login: string;
  password: string;

  constructor(public router: Router) {
  }

  ngOnInit() {
    let token: string = localStorage.getItem("token");

    if (token != "") {
      this.router.navigate(['home']);
    }
  }

  openRegistration() {
    this.registrationIsOpen = true;
  }

  closeAuthorization() {
    this.authorizationIsOpen = false;
  }

  signIn() {

  }
}
