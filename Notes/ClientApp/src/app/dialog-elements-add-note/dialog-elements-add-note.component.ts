import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dialog-elements-add-note',
  templateUrl: './dialog-elements-add-note.component.html',
  styleUrls: ['./dialog-elements-add-note.component.css']
})
export class DialogElementsAddNoteComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  categories = [{'name': 'SF'}, {'name': 'NYC'}, {'name': 'Buffalo'}];
  selectedCity = this.categories[1];
  description: any;
  selectedCategory: any;
  header: any;

  onChange(category) {
    console.log(category.name);
  }

  addNote() {
    
  }
}
