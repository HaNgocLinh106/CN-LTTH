import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TasksTestComponent } from './tasks-test.component';

describe('TasksTestComponent', () => {
  let component: TasksTestComponent;
  let fixture: ComponentFixture<TasksTestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TasksTestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TasksTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
