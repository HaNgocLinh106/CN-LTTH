
import { AddOrEditTaskModelComponent } from './add-or-edit-task-model.component';
import { TaskListDto, TaskServiceProxy, TaskState, UpdateTaskInput } from '@shared/service-proxies/service-proxies';
import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { result } from 'lodash-es';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent extends AppComponentBase implements OnInit {
  tasks:TaskListDto[]=[];
  taskEdit: TaskListDto;
  taskDelete:TaskListDto;
  selectedState: TaskState;
  stateSelectedOptions=[
    {text: this.l('AllTasks'), value: undefined},
    {text: this.l('TaskState_Open'), value: TaskState._1},
    {text: this.l('TaskState_Close'), value: TaskState._0}
  ];
    @ViewChild('addOrEditTaskModal') addOrEditTaskModal:AddOrEditTaskModelComponent;
    @ViewChild('deleteTaskModal') deleteTaskModal:ModalDirective;
  constructor(injector:Injector, private taskService: TaskServiceProxy) { 
    super(injector);
  }

  ngOnInit(): void {
   this.getTasks();
  }
  getTasks(){
    this.taskService.getAll(this.selectedState as any).subscribe(result =>{
      this.tasks= result.items;
    })
  }
  getTask(task:TaskListDto){
      this.taskService.getTask(task.id)
      .subscribe(result =>{
       this.setTaskEdit(result);
       
      });
      
      this.showTaskModal();
     
  }
  deleteTask(task:TaskListDto){
    this.deleteTaskModal.show();
    this.taskDelete= task;
    console.log("task 1=",this.taskDelete )
  }
  setTaskEdit(value: any) {
    this.taskEdit = value;
    console.log("task 1=", this.taskEdit);
}
  getTaskLabel(task: TaskListDto){
    return task?.state ===TaskState._1
          ?'label-success'
          :'label-default';
  }
  getTaskState(task:TaskListDto){
    switch(task.state){
      case TaskState._1:
        return this.l('TaskState_Open');
      case TaskState._0:
        return this.l('TaskState_Close');
      defult:
      return '';
    }
  }
  showTaskModal(){
    this.addOrEditTaskModal.Show();
  }
 onTaskUpdated(input:any){
   if(this.taskEdit==null){
    this.tasks.push(input);
   
   }
   this.getTasks();
   this.notify.success(this.l('SavedSuccessully'));
 }
 cancel(){
  this.deleteTaskModal.hide();
}
accept(){
    this.taskService.delete(this.taskDelete.id)
    .subscribe(result =>{
      this.deleteTaskModal.hide();
      this.getTasks();
      this.notify.success(this.l('DeletedSuccessully'));
    });

}
}
