import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dialog-elements-add-note',
  templateUrl: './dialog-elements-add-note.component.html',
  styleUrls: ['./dialog-elements-add-note.component.css']
})
export class DialogElementsAddNoteComponent implements OnInit {
  categories = [{'name': 'SF'}, {'name': 'NYC'}, {'name': 'Buffalo'}];
  selectedCity = this.categories[1];
  description: string;
  selectedCategory: string;
  header: string;
  imagePath: string;

  constructor() { }

  ngOnInit() {
  }

  onChange(category) {
    console.log(category.name);
  }

  addNote() {

  }
}
