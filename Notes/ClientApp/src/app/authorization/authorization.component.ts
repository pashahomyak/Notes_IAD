import { Component, OnInit } from '@angular/core';
import { DataService } from '../_services/data.service';
import { Router, ActivatedRoute } from '@angular/router';
import { User } from '../_models/user.model';
import { ServiceResponce } from '../_models/service-responce.model';

@Component({
  selector: 'app-authorization',
  templateUrl: './authorization.component.html',
  styleUrls: ['./authorization.component.css'],
  providers: [DataService]
})
export class AuthorizationComponent implements OnInit {
  login: string;
  password: string;

  responceIsReceived: boolean;

  serviceResponce: ServiceResponce;

  constructor(private dataService: DataService) { }

  ngOnInit() {
    this.responceIsReceived = false;
  }

  signIn() {
    if (this.login == "" || this.password == "") {
      //error
    } else {
      let user: User = new User(this.login, this.password, null);

      this.dataService.getData("signIn", user)
        .subscribe((data: ServiceResponce) => this.serviceResponce = data);

      //this.router.navigate([''], { relativeTo: this.route });
      this.responceIsReceived = true;
    }
  }

  saveToken() {
    localStorage.setItem("token", this.serviceResponce.data);
  }
}
