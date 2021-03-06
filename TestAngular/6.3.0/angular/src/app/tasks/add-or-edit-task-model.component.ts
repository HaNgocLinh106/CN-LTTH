import { CreateTaskInput, TaskListDto, UpdateTaskInput } from './../../shared/service-proxies/service-proxies';
import { TaskServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from 'shared/app-component-base';
import { Component, OnInit, Output, ViewChild, EventEmitter, Injector, Input } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from "rxjs/operators";
@Component({
  selector: 'addOrEditTaskModal',
  templateUrl: './add-or-edit-task-model.component.html',
  styleUrls: ['./add-or-edit-task-model.component.css']
})
export class AddOrEditTaskModelComponent extends AppComponentBase implements OnInit {
@ViewChild('createOrEditModal') modal:ModalDirective;
@Input() taskEdit:UpdateTaskInput;
@Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
active= false;
saving= false;
task: CreateTaskInput= new CreateTaskInput();
  constructor(injector:Injector, private taskService: TaskServiceProxy) { 
    super(injector);
      this.taskEdit=null;
  }

  Show():void{
    this.active= true;
    this.modal.show();
  }
  save():void{
    this.saving=true;
    if(this.taskEdit==null){
      this.taskService.create(this.task)
      .pipe(
        finalize(()=>{
        this.saving=false;
      }))
      .subscribe(result =>{
        this.modalSave.emit(result);
        this.close();
      });
    }
    else{
      this.taskService.update(this.taskEdit)
      .pipe(
        finalize(()=>{
        this.saving=false;
      }))
      .subscribe(result =>{
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
