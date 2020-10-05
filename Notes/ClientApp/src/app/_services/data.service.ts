import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import {User} from "../_models/user.model";

@Injectable()
export class DataService {
  constructor(private http: HttpClient) {
  }

  getData(myUrl: string, user) {
    let url: string = "/users/" + myUrl;

    return this.http.post(url, user);
  }
}
