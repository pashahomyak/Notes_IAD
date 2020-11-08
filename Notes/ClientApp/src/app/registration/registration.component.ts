import { Component, OnInit } from '@angular/core';
import {DataService} from "../_services/data.service";
import {User} from "../_models/user.model";
import {Router, RouterLink, ActivatedRoute} from "@angular/router";
import { ServiceResponce } from '../_models/service-responce.model';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
  providers: [DataService]
})
export class RegistrationComponent implements OnInit {
  login: string;
  email: string;
  password: string;

  responceIsReceived: boolean;

  serviceResponce: ServiceResponce;

  constructor(private dataService: DataService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.responceIsReceived = false;
  }

  signUp() {
    let user: User = new User(this.login, this.password, this.email);

    this.dataService.getData("/users/", "signUp", user)
      .subscribe((data: ServiceResponce) => {
        this.serviceResponce = data;
      });

    this.responceIsReceived = true;
  }

  saveToken() {
    localStorage.setItem("token", this.serviceResponce.data);
  }

  goHome() {
    this.router.navigate(['home']);
  }
}
