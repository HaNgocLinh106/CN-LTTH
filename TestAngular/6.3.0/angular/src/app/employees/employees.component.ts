
import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { EmployeeListDto, EmployeeServiceProxy } from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AddOrEditEmployeeComponent } from './add-or-edit-employee.component';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent extends AppComponentBase implements OnInit {
  employees:EmployeeListDto[]=[];
  employeeEdit: EmployeeListDto;
  taskDelete:EmployeeListDto;

     @ViewChild('addOrEditEmployeeModal') addOrEditEmployeeModal:AddOrEditEmployeeComponent;
    @ViewChild('deleteEmployeeModal') deleteEmployeeModal:ModalDirective;
  constructor(injector:Injector, private taskService: EmployeeServiceProxy) { 
    super(injector);
  }

  ngOnInit(): void {
   this.getTasks();
  }
  getTasks(){
    this.taskService.getAll().subscribe(result =>{
      this.employees= result.items;
    })
  }
//   getTask(employee:EmployeeListDto){
//       this.taskService.getEmployee(employee.id)
//       .subscribe(result =>{
//        this.setEmployeeEdit(result);
       
//       });
      
//       this.showTaskModal();
     
//   }
//   deleteEmployee(task:EmployeeListDto){
//     this.deleteEmployeeModal.show();
//     this.taskDelete= task;
//     console.log("task 1=",this.taskDelete )
//   }
//   setEmployeeEdit(value: any) {
//     this.employeeEdit = value;
//     console.log("task 1=", this.employeeEdit);
// }
 
  showEmployeeModal(){
    this.addOrEditEmployeeModal.Show();
  }
 onTaskUpdated(input:any){
   if(this.employeeEdit==null){
    this.employees.push(input);
   
   }
   this.getTasks();
   this.notify.success(this.l('SavedSuccessully'));
 }
//  cancel(){
//   this.deleteTaskModal.hide();
// }
// accept(){
//     this.taskService.delete(this.taskDelete.id)
//     .subscribe(result =>{
//       this.deleteTaskModal.hide();
//       this.getTasks();
//       this.notify.success(this.l('DeletedSuccessully'));
//     });

// }
}

