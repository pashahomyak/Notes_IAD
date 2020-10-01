import { Component } from '@angular/core';
import {trigger} from "@angular/animations";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  authorizationIsOpen: boolean = false;
  registrationIsOpen: boolean = false;
  login: string;
  password: string;



  openRegistration() {
    this.registrationIsOpen = true;
  }

  closeAuthorization() {
    this.authorizationIsOpen = false;
  }

  signIn() {

  }
}
