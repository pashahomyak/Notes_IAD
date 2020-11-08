import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import {User} from "../_models/user.model";

@Injectable()
export class DataService {
  constructor(private http: HttpClient) {
  }

  getData(page: string, myUrl: string, data) {
    let url: string = page + myUrl;

    return this.http.post(url, data);
  }
}
