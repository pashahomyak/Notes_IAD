import { Component, OnInit } from '@angular/core';
import {DataService} from "../_services/data.service";
import {ServiceResponce} from "../_models/service-responce.model";
import {ModificationModel} from "../_models/modification.model";

@Component({
  selector: 'app-dialog-elements-password',
  templateUrl: './dialog-elements-password.component.html',
  styleUrls: ['./dialog-elements-password.component.css'],
  providers: [DataService]
})
export class DialogElementsPasswordComponent implements OnInit {
  oldPassword: string;
  newPassword: string;
  serviceResponce: ServiceResponce;
  responceIsReceived: boolean;

  constructor(private dataService: DataService) { }

  ngOnInit() {
    this.responceIsReceived = false;
  }

  change() {
    let modificationModel: ModificationModel = new ModificationModel(
      localStorage.getItem("token"),
      this.oldPassword,
      this.newPassword
    );

    this.dataService.getData("/users/", "changePassword", modificationModel)
      .subscribe((data: ServiceResponce) => {
        this.serviceResponce = data;
      });

    this.responceIsReceived = true;
  }

  updateToken() {
    localStorage.setItem("token", this.serviceResponce.data)
  }

}
