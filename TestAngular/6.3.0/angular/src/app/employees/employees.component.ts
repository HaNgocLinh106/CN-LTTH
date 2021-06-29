import { result } from 'lodash-es';
import { EmployeeServiceProxy, EmployeeListDto } from './../../shared/service-proxies/service-proxies';
import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { AddOrEditEmployeeComponent } from './add-or-edit-employee.component';


@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent extends AppComponentBase implements OnInit {
  employees: EmployeeListDto[]=[];
   @ViewChild('addOrEditEmployeeModal') addOrEditEmployeeModal:AddOrEditEmployeeComponent;
  constructor(injector:Injector, private employeeService:EmployeeServiceProxy) {
    super(injector);
   }

  ngOnInit(): void {
    this.getEmployees();
  }
  getEmployees(){
    this.employeeService.getAll()
    .subscribe(result=>{
      this.employees= result.items;
      console.log("employee=", this.employees);
    });
  }
  showTaskModal(){
     this.addOrEditEmployeeModal.Show();
  }
//  onTaskUpdated(input:any){
//    if(this.taskEdit==null){
//     this.tasks.push(input);
   
//    }
//    this.getTasks();
//    this.notify.success(this.l('SavedSuccessully'));
//  }
}
