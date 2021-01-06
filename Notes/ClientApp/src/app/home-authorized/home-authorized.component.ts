import { Component, OnInit } from '@angular/core';
import {NoteCategory} from "../_models/note-category.model";
import {DataService} from "../_services/data.service";
import {ServiceResponce} from "../_models/service-responce.model";
import {Token} from "../_models/token.model";
import {DialogElementsEmailComponent} from "../dialog-elements-email/dialog-elements-email.component";
import {MatDialog} from "@angular/material/dialog";
import {DialogElementsAddNoteComponent} from "../dialog-elements-add-note/dialog-elements-add-note.component";
import {DialogElementsCategorySettingsComponent} from "../dialog-elements-category-settings/dialog-elements-category-settings.component";
import {Notes} from "../_models/notes.model";
import {Router} from "@angular/router";
import {Note} from "../_models/note.model";

@Component({
  selector: 'app-home-authorized',
  templateUrl: './home-authorized.component.html',
  styleUrls: ['./home-authorized.component.css'],
  providers: [DataService]
})
export class HomeAuthorizedComponent implements OnInit {
  noteCategories: NoteCategory;
  notes: Notes;
  favoritesPath: string = "assets/images/star1.png";

  constructor(private dataService: DataService, public dialog: MatDialog) {
  }

  ngOnInit() {
    this.getNoteCategories();
    this.getMainNotes();
  }

  changeFavoritesState(note: Note) {
    this.dataService.getData("/notes/", "changeFavoritesState", note)
      .subscribe((data: Notes) => this.notes = data);

    /*if (this.favoritesPath === "assets/images/star1.png") {
      this.favoritesPath = "assets/images/star11.png";
    } else {
      this.favoritesPath = "assets/images/star1.png";
    }*/
  }

  private getNoteCategories() {
    let token: Token = new Token(localStorage.getItem("token"));

    this.dataService.getData("/noteCategories/", "getNoteCategories", token)
      .subscribe((data: NoteCategory) => this.noteCategories = data);
  }

  getMainNotes() {
    let token: Token = new Token(localStorage.getItem("token"));

    this.dataService.getData("/notes/", "getMainNotes", token)
      .subscribe((data: Notes) => {this.notes = data; console.log(this.notes)});
  }

  getFavoritesNotes() {
    this.dataService.getData("/notes/", "getFavoritesNotes", "")
      .subscribe((data: Notes) => {this.notes = data;});
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

  deleteCurrentNote(id: number) {
    this.dataService.getData("/notes/", "deleteNote", id)
      .subscribe((data: string) => {this.ngOnInit()});
  }

  loadCateGoryNotes(category: string) {
    this.dataService.getData("/notes/", "getNotesByCategory", new Token(category))
      .subscribe((data: Notes) => {this.notes = data;});
  }
}
