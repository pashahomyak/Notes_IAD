import { Component, OnInit } from '@angular/core';
import {NoteCategory} from "../_models/note-category.model";
import {DataService} from "../_services/data.service";
import {ServiceResponce} from "../_models/service-responce.model";
import {Token} from "../_models/token.model";

@Component({
  selector: 'app-home-authorized',
  templateUrl: './home-authorized.component.html',
  styleUrls: ['./home-authorized.component.css'],
  providers: [DataService]
})
export class HomeAuthorizedComponent implements OnInit {
  noteCategories: NoteCategory;

  constructor(private dataService: DataService) {
  }

  ngOnInit() {
    this.getNoteCategories()
  }

  private getNoteCategories() {
    let token: Token = new Token(localStorage.getItem("token"));

    this.dataService.getData("getNoteCategories", token)
      .subscribe((data: NoteCategory) => this.noteCategories = data);
  }

}
