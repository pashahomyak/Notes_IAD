import { Component, OnInit } from '@angular/core';
import {DataService} from "../_services/data.service";
import {ServiceResponce} from "../_models/service-responce.model";
import {Token} from "../_models/token.model";
import {ModificationModel} from "../_models/modification.model";

@Component({
  selector: 'app-dialog-elements-email',
  templateUrl: './dialog-elements-email.component.html',
  styleUrls: ['./dialog-elements-email.component.css'],
  providers: [DataService]
})
export class DialogElementsEmailComponent implements OnInit {
  oldEmail: string;
  newEmail: string;
  serviceResponce: ServiceResponce;
  responceIsReceived: boolean;

  constructor(private dataService: DataService) { }

  ngOnInit() {
    this.responceIsReceived = false;
  }

  change() {
    let modificationModel: ModificationModel = new ModificationModel(
      localStorage.getItem("token"),
      this.oldEmail,
      this.newEmail
    );

    this.dataService.getData("/users/","changeEmail", modificationModel)
      .subscribe((data: ServiceResponce) => {
        this.serviceResponce = data;
      });

    this.responceIsReceived = true;
  }

  updateToken() {
    localStorage.setItem("token", this.serviceResponce.data)
  }
}
