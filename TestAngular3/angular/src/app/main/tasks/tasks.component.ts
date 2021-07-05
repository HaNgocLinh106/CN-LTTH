

import { EmployeeListDto, EmployeeServiceProxy, TaskListDto, TaskServiceProxy, UpdateTaskInput } from '@shared/service-proxies/service-proxies';
import { Component, OnInit, Injector, ViewChild } from '@angular/core';


import { ModalDirective } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TaskState } from './TaskState';
import { AddOrEditTaskModelComponent } from './add-or-edit-task-model.component';


@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})

export class TasksComponent extends AppComponentBase implements OnInit {
  
  tasks:TaskListDto[]=[];
  taskEdit: UpdateTaskInput;
  employees: EmployeeListDto[]=[];
  taskDelete:TaskListDto;
  selectedState: TaskState;
  selectedEmployee:number;
  checkEmployee:boolean;
  stateSelectedOptions=[
    {text: this.l('AllTasks'), value: undefined},
    {text: this.l('TaskState_Close'), value: TaskState.Completed},
    {text: this.l('TaskState_Open'), value: TaskState.Open}
  ];
     @ViewChild('addOrEditTaskModal') addOrEditTaskModal:AddOrEditTaskModelComponent;
    @ViewChild('deleteTaskModal') deleteTaskModal:ModalDirective;
    @ViewChild('listEmployee') listEmployee:ModalDirective;
  constructor(injector:Injector, private taskService: TaskServiceProxy, private employeeService: EmployeeServiceProxy) { 
    super(injector);
    this.checkEmployee=false;
  }

  ngOnInit(): void {
   this.getTasks();
   this.getEmployees();
  }
  getTasks(){
    this.taskService.getAll(this.selectedState as any).subscribe(result =>{
      this.tasks= result.items;
    })
  }
  getEmployees(){
    this.employeeService.getAll()
    .subscribe( result =>{
      this.employees= result.items;
      console.log("emp=", this.employees);
    })
  }
  getTask(task:TaskListDto, i:number){
  
      this.taskService.getTask(task.id)
      .subscribe(result =>{
       this.setTaskEdit(result);
       
      });
      switch(i){
        case 0:
          return this.showListEmployee();
        default:
        return this.showTaskModal();
      }
     
     
  }
  deleteTask(task:TaskListDto){
    this.deleteTaskModal.show();
    this.taskDelete= task;
  }
  setTaskEdit(value: any) {
    this.taskEdit = value;
}
  getTaskLabel(task: TaskListDto){
    return task.state === <any>TaskState.Open
          ? 'label-success'
          : 'label-default';
  }
  getTaskState(task:TaskListDto){
    switch(task.state){
      case <any>TaskState.Completed:
        return this.l('TaskState_Close');
      case <any>TaskState.Open:
        return this.l('TaskState_Open');
      default:
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
      this.checkEmployee=false;
    });

}
showListEmployee(){
  this.listEmployee.show();
}
saveTaskAssigned():void{
   this.taskEdit.assignedEmployeeId=this.selectedEmployee;
    this.taskService.update(this.taskEdit)
    .subscribe(result =>{
      this.close();
      this.getTasks();
      this.notify.success(this.l('AssignedSuccessully'));
      this.taskEdit=null;
    });
}
close(){

  this.listEmployee.hide();
}
}
