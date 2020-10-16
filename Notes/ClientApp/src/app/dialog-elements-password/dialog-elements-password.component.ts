import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dialog-elements-password',
  templateUrl: './dialog-elements-password.component.html',
  styleUrls: ['./dialog-elements-password.component.css']
})
export class DialogElementsPasswordComponent implements OnInit {
  oldPassword: any;
  newPassword: any;

  constructor() { }

  ngOnInit() {
  }

}
