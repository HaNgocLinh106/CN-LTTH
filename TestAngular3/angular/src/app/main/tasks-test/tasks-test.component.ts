import { AssignTaskInput2, EmployeeListAssignOutput } from './../../../shared/service-proxies/service-proxies';


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
  employees: EmployeeListAssignOutput[]=[];
  taskDelete:TaskListDto;
  selectedState: TaskState;
  selectedEmployee:number;
  selectEmployee:boolean;
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
    this.selectEmployee= false;
    this.selectedEmployeeIDs=[];
  }

  ngOnInit(): void {
   this.getTasks();
  }
  getTasks(){
    this.taskService.getAllTaskDetail(this.selectedState as any).subscribe(result =>{
      this.tasks= result;
    })
  }
  getTask(task:TaskListDto2, i:number){
  
      this.taskService.getTask(task.id)
      .subscribe(result =>{
       this.setTaskEdit(result);
       this.taskAssign.id=result.id;
      });
      switch(i){
        case 0:
          {
            this.employeeService.getListEmployeeAssign(task.id)
            .subscribe( result =>{
              this.employees= result;
              result.forEach((obj)=>{
                if(!obj.selected)
                this.selectedEmployeeIDs.push(obj.id);
              })
              console.log("result =",  this.selectedEmployeeIDs);
              this.listEmployee.show();
            })
           break;
          };
        default:
        return this.showTaskModal();
      }
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
  setTaskEdit(value: any) {
    this.taskEdit= value;
}
  setTaskAssign(value: number) {
  this.taskAssign.id= value;
}
  showTaskModal(){
      this.addOrEditTaskTestModal.show();
  }
  showListEmployee(){
    console.log("employee=", this.employees);
    this.listEmployee.show();
  }
  
 onTaskUpdated(input:any){
   if(this.taskEdit==null){
    this.tasks.push(input);
   
   }
   this.getTasks();
   this.notify.success(this.l('SavedSuccessully'));
 }
 deleteTask(task:TaskListDto){
  this.deleteTaskModal.show();
  this.taskDelete= task;
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
      this.selectEmployee= false;
      this.selectedEmployeeIDs=[];
    });

}

close(){
  this.selectedEmployeeIDs=[];
  this.listEmployee.hide();
}
selectID(task: TaskListDto2, event:any){
  if(event.target.checked){
    this.selectedEmployeeIDs.push(task.id);
    console.log("employee duoc chon=", task.id);
  }
  else{
    console.log("employee xoa=", task.id);
    let index = this.selectedEmployeeIDs.indexOf(task.id);
    this.selectedEmployeeIDs.splice(index,1); 
  }

}
saveTaskAssigned():void{
console.log("list employeeId=",this.selectedEmployeeIDs);
this.taskAssign.employeeIds= this.selectedEmployeeIDs;
console.log("sua  nay =",this.taskAssign);
 this.taskService.assignTask(this.taskAssign)
 .subscribe(result =>{
   this.close();
   this.getTasks();
   this.notify.success(this.l('AssignedSuccessully'));
   this.taskEdit=null;
   this.selectedEmployeeIDs=[];
   this.selectEmployee=false;
 });
}
}
