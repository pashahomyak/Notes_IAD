import { Component, OnInit } from '@angular/core';
import {User} from "../_models/user.model";
import {HttpClient} from "@angular/common/http";
import {DataService} from "../_services/data.service";
import {ServiceResponce} from "../_models/service-responce.model";
import {Token} from "../_models/token.model";
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import {DialogElementsEmailComponent} from "../dialog-elements-email/dialog-elements-email.component";
import {DialogElementsPasswordComponent} from "../dialog-elements-password/dialog-elements-password.component";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
  providers: [DataService]
})
export class ProfileComponent implements OnInit {
  login: string;
  email: string;
  userDto: User;
  dataIsObtained: boolean;

  constructor(private dataService: DataService, public dialog: MatDialog) {
  }

  ngOnInit() {
    this.dataIsObtained = false;

    this.getProfileData();
  }
  changeEmail() {
    this.dialog.open(DialogElementsEmailComponent, {
      //height: '400px',
      width: '600px',
    });
  }
  changePassword() {
    this.dialog.open(DialogElementsPasswordComponent);
  }
  getProfileData() {
    let token: Token = new Token(localStorage.getItem("token"));

    this.dataService.getData("getProfileData", token)
      .subscribe((data: User) => {this.userDto = data; this.dataIsObtained = true;});
  }
  fillProfileData() {
    this.login = this.userDto.login;
    this.email = this.userDto.email;
  }
}
