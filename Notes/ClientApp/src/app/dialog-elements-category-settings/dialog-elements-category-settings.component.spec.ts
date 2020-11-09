import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogElementsCategorySettingsComponent } from './dialog-elements-category-settings.component';

describe('DialogElementsCategorySettingsComponent', () => {
  let component: DialogElementsCategorySettingsComponent;
  let fixture: ComponentFixture<DialogElementsCategorySettingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DialogElementsCategorySettingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogElementsCategorySettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
