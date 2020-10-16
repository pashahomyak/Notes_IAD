import { Component, OnInit } from '@angular/core';
import {DataService} from "../_services/data.service";
import {ServiceResponce} from "../_models/service-responce.model";
import {Token} from "../_models/token.model";

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

  constructor(private dataService: DataService) { }

  ngOnInit() {
  }

  change() {
    let token: Token = new Token(localStorage.getItem("token"));

    this.dataService.getData("changeEmail", token)
      .subscribe((data: ServiceResponce) => {
        this.serviceResponce = data;
      });
  }
}
