import { Component, OnInit } from '@angular/core';
import {NoteCategory} from "../_models/note-category.model";
import {DataService} from "../_services/data.service";
import {ServiceResponce} from "../_models/service-responce.model";
import {Token} from "../_models/token.model";
import {DialogElementsEmailComponent} from "../dialog-elements-email/dialog-elements-email.component";
import {MatDialog} from "@angular/material/dialog";
import {DialogElementsAddNoteComponent} from "../dialog-elements-add-note/dialog-elements-add-note.component";
import {DialogElementsCategorySettingsComponent} from "../dialog-elements-category-settings/dialog-elements-category-settings.component";

@Component({
  selector: 'app-home-authorized',
  templateUrl: './home-authorized.component.html',
  styleUrls: ['./home-authorized.component.css'],
  providers: [DataService]
})
export class HomeAuthorizedComponent implements OnInit {
  noteCategories: NoteCategory;

  constructor(private dataService: DataService, public dialog: MatDialog) {
  }

  ngOnInit() {
    this.getNoteCategories()
  }

  private getNoteCategories() {
    let token: Token = new Token(localStorage.getItem("token"));

    this.dataService.getData("/noteCategories/", "getNoteCategories", token)
      .subscribe((data: NoteCategory) => this.noteCategories = data);
  }

  getMainNotes() {

  }

  getFavoritesNotes() {

  }

  addNote() {
    this.dialog.open(DialogElementsAddNoteComponent, {
      //height: '400px',
      width: '600px',
    });
  }

  openCategorySettings() {
    this.dialog.open(DialogElementsCategorySettingsComponent, {
      height: '400px',
      width: '600px',
    });
  }
}
