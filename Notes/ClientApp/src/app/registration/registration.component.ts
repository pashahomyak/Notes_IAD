import { Component, OnInit } from '@angular/core';
import {DataService} from "../_services/data.service";
import {User} from "../_models/user.model";
import {Router, RouterLink, ActivatedRoute} from "@angular/router";

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

  isRegistered: boolean;

  postResult: string;

  constructor(private dataService: DataService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.isRegistered = false;
  }

  signUp() {
    if (this.login == "" || this.password == "" || this.email == "") {
      //error
    } else {
      let user: User = new User(this.login, this.password, this.email);

      this.dataService.getData("signUp", user)
        .subscribe((data: string) => this.postResult = data);

      console.log(this.postResult);

      if (this.postResult == "loginExist") {
        //error
        this.isRegistered = false;
      } else {
        //ok
        this.isRegistered = true;
        //this.router.navigate([''], { relativeTo: this.route });
      }
    }
  }
}
