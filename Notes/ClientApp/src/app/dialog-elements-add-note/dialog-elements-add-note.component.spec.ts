import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogElementsAddNoteComponent } from './dialog-elements-add-note.component';

describe('DialogElementsAddNoteComponent', () => {
  let component: DialogElementsAddNoteComponent;
  let fixture: ComponentFixture<DialogElementsAddNoteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DialogElementsAddNoteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogElementsAddNoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
