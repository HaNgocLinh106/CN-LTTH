import { AssignTaskInput2 } from './../../../shared/service-proxies/service-proxies';


import { EmployeeListDto, EmployeeServiceProxy, Task2ServiceProxy, TaskListDto, TaskListDto2, UpdateTaskInput, UpdateTaskInput2, TaskServiceProxy } from '@shared/service-proxies/service-proxies';
import { Component, OnInit, Injector, ViewChild } from '@angular/core';


import { ModalDirective } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/common/app-component-base';

import { TaskState } from '../tasks/TaskState';
import { AddOrEditTaskTestModelComponent } from './add-or-edit-task-test-model.component';


@Component({
  selector: 'app-tasks-test',
  templateUrl: './tasks-test.component.html',
  styleUrls: ['./tasks-test.component.css']
})

export class TasksTestComponent extends AppComponentBase implements OnInit {
  
  tasks:TaskListDto2[]=[];
  taskEdit: UpdateTaskInput2;
  taskAssign: AssignTaskInput2 = new AssignTaskInput2();
  employees: EmployeeListDto[]=[];
  taskDelete:TaskListDto;
  selectedState: TaskState;
  selectedEmployee:number;
  checkEmployee:boolean;
  selectedEmployeeIDs:number[]=[];
  stateSelectedOptions=[
    {text: this.l('AllTasks'), value: undefined},
    {text: this.l('TaskState_Close'), value: TaskState.Completed},
    {text: this.l('TaskState_Open'), value: TaskState.Open}
  ];
      @ViewChild('addOrEditTaskTestModal') addOrEditTaskTestModal:AddOrEditTaskTestModelComponent;
    @ViewChild('deleteTaskModal') deleteTaskModal:ModalDirective;
    @ViewChild('listEmployee') listEmployee:ModalDirective;
  constructor(injector:Injector, private taskService: Task2ServiceProxy,private task:TaskServiceProxy, private employeeService: EmployeeServiceProxy) { 
    super(injector);
    this.checkEmployee=false;
    this.taskAssign.employeeIds=[13,12];
    
  }

  ngOnInit(): void {
   this.getTasks();
   this.getEmployees();
   this.taskAssign.employeeIds=[13,12];
  }
  getTasks(){
    this.taskService.getAllTaskDetail(this.selectedState as any).subscribe(result =>{
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
  getTask(task:TaskListDto2, i:number){
  
      this.taskService.getTask(task.id)
      .subscribe(result =>{
       this.setTaskEdit(result);
      //  this.setTaskAssign(result.id);
       
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
  
    this.taskEdit= value;
    this.taskAssign=value;
    // console.log("id duoc truyen=", this.taskAssign.id);
}
setTaskAssign(value: number) {
  
  this.taskAssign.id= value;
  // console.log("id duoc truyen=", this.taskAssign.id);
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
      this.addOrEditTaskTestModal.show();
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
selectID(task: TaskListDto2, event:any){
    if(event.target.checked){
      this.selectedEmployeeIDs.push(task.id);
      console.log("employee duoc chon=", task.id);
    }
    else{
      // this.masterCheck=false;
      console.log("employee xoa=", task.id);
      let index = this.selectedEmployeeIDs.indexOf(task.id);
      this.selectedEmployeeIDs.splice(index,1); 
    }
 
}
saveTaskAssigned():void{
  console.log("list employeeId=",this.selectedEmployeeIDs);
  this.taskAssign.employeeIds= this.selectedEmployeeIDs;
  this.taskAssign.employeeIds=[13];
  console.log("sua  nay =",this.taskAssign);
  // this.taskEdit.assignedEmployeeId=this.selectedEmployee;
   this.taskService.assignTask(this.taskAssign)
   .subscribe(result =>{
     this.close();
     this.getTasks();
     this.notify.success(this.l('AssignedSuccessully'));
     this.taskEdit=null;
     this.taskAssign=null;
   });
}
close(){

  this.listEmployee.hide();
}
}
