
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
  employeeDelete:EmployeeListDto;

     @ViewChild('addOrEditEmployeeModal') addOrEditEmployeeModal:AddOrEditEmployeeComponent;
    @ViewChild('deleteEmployeeModal') deleteEmployeeModal:ModalDirective;
  constructor(injector:Injector, private employeeService: EmployeeServiceProxy) { 
    super(injector);
  }

  ngOnInit(): void {
   this.getEmployees();
  }
  getEmployees(){
    this.employeeService.getAll().subscribe(result =>{
      this.employees= result.items;
    })
  }
  getEmployee(employee:EmployeeListDto){
      this.employeeService.getEmployee(employee.id)
      .subscribe(result =>{
       this.setEmployeeEdit(result);
       
      });
      this.showEmployeeModal();
     
  }
  deleteEmployee(employee:EmployeeListDto){
    this.deleteEmployeeModal.show();
    this.employeeDelete= employee;
  }
  setEmployeeEdit(value: any) {
    this.employeeEdit = value;
     this.employeeEdit.birthDate=value["birthDate"]? value["birthDate"].format("YYYY-MM-DD"):<any>undefined;
}
 
  showEmployeeModal(){
    this.addOrEditEmployeeModal.Show();
  }
 onEmployeeUpdated(input:any){
   if(this.employeeEdit==null){
    this.employees.push(input);
   
   }
   this.getEmployees();
   this.notify.success(this.l('SavedSuccessully'));
 }
 cancel(){
  this.deleteEmployeeModal.hide();
}
accept(){
    this.employeeService.deleteEmployee(this.employeeDelete.id)
    .subscribe(result =>{
      this.deleteEmployeeModal.hide();
      this.getEmployees();
      this.notify.success(this.l('DeletedSuccessully'));
    });

}
}

