import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  login: string;
  email: string;

  constructor() { }

  ngOnInit() {
  }

  changeEmail() {

  }

  changePassword() {

  }
}
