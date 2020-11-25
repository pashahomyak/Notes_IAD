import { Component, OnInit } from '@angular/core';
import {DataService} from "../_services/data.service";
import {Token} from "../_models/token.model";
import {Notes} from "../_models/notes.model";
import {NoteCategory} from "../_models/note-category.model";

@Component({
  selector: 'app-dialog-elements-add-note',
  templateUrl: './dialog-elements-add-note.component.html',
  styleUrls: ['./dialog-elements-add-note.component.css'],
  providers: [DataService]
})
export class DialogElementsAddNoteComponent implements OnInit {
  categories = [{'name': 'Main'}];
  selectedCity = this.categories[1];
  description: string;
  selectedCategory: string;
  header: string;
  imagePath: string;

  userCategories: string[];

  constructor(private dataService: DataService) { }

  ngOnInit() {
    this.loadCategories();
  }

  onChange(category) {
    console.log(category.name);
  }

  addNote() {

  }

  loadCategories() {
    let token: Token = new Token(localStorage.getItem("token"));

    this.dataService.getData("/noteCategories/", "getNoteCategories", token)
      .subscribe((data: NoteCategory) => {
        this.userCategories = data.categories;
        for (var index in this.userCategories) {
          this.categories.push({'name': this.userCategories[index]});
        }
      });
  }
}
