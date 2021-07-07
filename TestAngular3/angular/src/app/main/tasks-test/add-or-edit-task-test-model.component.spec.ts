import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddOrEditTaskTestModelComponent } from './add-or-edit-task-test-model.component';

describe('AddOrEditTaskTestModelComponent', () => {
  let component: AddOrEditTaskTestModelComponent;
  let fixture: ComponentFixture<AddOrEditTaskTestModelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddOrEditTaskTestModelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddOrEditTaskTestModelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
