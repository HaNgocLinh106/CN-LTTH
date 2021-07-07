
import { CreateTaskInput, CreateTaskInput2, Task2ServiceProxy, TaskServiceProxy, UpdateTaskInput, UpdateTaskInput2 } from '@shared/service-proxies/service-proxies';

import { Component, OnInit, Output, ViewChild, EventEmitter, Injector, Input, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TaskState } from '../tasks/TaskState';

@Component({
  selector: 'addOrEditTaskTestModal',
  templateUrl: './add-or-edit-task-model.component.html',
  styleUrls: ['./add-or-edit-task-model.component.css']
})
export class AddOrEditTaskModelComponent extends AppComponentBase implements OnInit {
@ViewChild('createOrEditModal') modal:ModalDirective;
@ViewChild('taskState') taskState: ElementRef;
@Input() taskEdit:UpdateTaskInput2;
@Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
active= false;
saving= false;
 selectedOption:number;

task: CreateTaskInput2= new CreateTaskInput2();
stateSelectedOptions=[
  {text: this.l('TaskState_Open'), value: TaskState.Open},
  {text: this.l('TaskState_Close'), value: TaskState.Completed}
];

  constructor(injector:Injector, private taskService: Task2ServiceProxy) { 
    super(injector);
        // this.taskEdit.state;
  }

  show():void{
    this.active= true;
    this.modal.show();
    console.log("task state=", this.taskState);
    console.log("task edit=", this.taskEdit);
   
  }
  setState(state){
    // this.taskEdit.state= state;
    console.log("state=", state);
  }
  save():void{
    this.saving=true;
    if(this.taskEdit==null){
      this.taskService.create(this.task)
      .subscribe(result =>{
        this.saving=false;
        this.modalSave.emit(result);
        this.close();
      });
    }
    else{
    
      //  this.taskEdit.state=this.selectedOption;
      console.log("modal co list task=", this.taskEdit);
      console.log("select  =",this.selectedOption);
      this.taskService.update(this.taskEdit)

      .subscribe(result =>{
        this.saving=false;
        this.modalSave.emit(result);
        this.close();
      });
    }
   
  }
  close(){
    this.active=false;
    this.modal.hide();
    this.taskEdit=null;
  }
  ngOnInit(): void {
  }

}
