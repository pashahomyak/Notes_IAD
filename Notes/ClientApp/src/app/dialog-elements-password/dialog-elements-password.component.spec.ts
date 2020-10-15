import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogElementsPasswordComponent } from './dialog-elements-password.component';

describe('DialogElementsPasswordComponent', () => {
  let component: DialogElementsPasswordComponent;
  let fixture: ComponentFixture<DialogElementsPasswordComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DialogElementsPasswordComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogElementsPasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
