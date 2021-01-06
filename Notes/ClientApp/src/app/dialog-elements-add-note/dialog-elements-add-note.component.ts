import { Component, OnInit } from '@angular/core';
import {DataService} from "../_services/data.service";
import {Token} from "../_models/token.model";
import {Notes} from "../_models/notes.model";
import {NoteCategory} from "../_models/note-category.model";
import {Note} from "../_models/note.model";
import {Router} from "@angular/router";
import {FormBuilder, Validators} from "@angular/forms";

@Component({
  selector: 'app-dialog-elements-add-note',
  templateUrl: './dialog-elements-add-note.component.html',
  styleUrls: ['./dialog-elements-add-note.component.css'],
  providers: [DataService, FormBuilder]
})
export class DialogElementsAddNoteComponent implements OnInit {
  categories = [{'name': 'Main'}];
  selectedCity = this.categories[1];
  description: string;
  selectedCategory: string;
  header: string;
  categoryName: string;

  userCategories: string[];

  result: string;

  imageData: string;

  private fileName: any;
  public formGroup = this.fb.group({
    file: [null, Validators.required]
  });

  constructor(private dataService: DataService, public router: Router, private fb: FormBuilder) { }

  ngOnInit() {
    this.loadCategories();
  }

  onChange(category) {
    this.categoryName = category.name;
  }

  addNote() {
    let note: Note = new Note(0, this.header, this.description, false, this.imageData, this.fileName, "", this.categoryName);

    this.dataService.getData("/notes/", "addNote", note)
      .subscribe((data: string) =>
      {
        this.result = data;

        this.router.navigate(['home']);
      });
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

  onFileChanged(event) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      this.fileName = event.target.files[0].name;
      const [file] = event.target.files;
      reader.readAsDataURL(file);

      reader.onload = () => {
        this.formGroup.patchValue({
          file: reader.result
        });
        this.imageData = this.formGroup.get('file').value.toString();
      };
    }

    console.log(this.imageData);
  }
}
