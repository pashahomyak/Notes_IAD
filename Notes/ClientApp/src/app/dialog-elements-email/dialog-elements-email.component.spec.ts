import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogElementsEmailComponent } from './dialog-elements-email.component';

describe('DialogElementsEmailComponent', () => {
  let component: DialogElementsEmailComponent;
  let fixture: ComponentFixture<DialogElementsEmailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DialogElementsEmailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogElementsEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
